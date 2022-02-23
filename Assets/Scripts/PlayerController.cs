using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Locomotion bools")]
    public bool isRunning;
    public bool isJumping;

    [Header("Weapon Variables")]
    public bool isAiming;

    [Header("Locomotion")]
    public float walkSpeed = 5;
    public float runSpeed = 8;
    public float jumpForce = 5;

    private float aimSensitivity = 1;

    public Animator anim;
    Rigidbody rb;
    public GameObject followTransform;

    Vector2 InputVector = Vector2.zero;
    Vector3 MoveDirection = Vector3.zero;
    Vector2 LookInput = Vector2.zero;
    
    public readonly int moveXHash = Animator.StringToHash("MovementX");
    public readonly int moveYHash = Animator.StringToHash("MovementY");
    public readonly int isJumpingHash = Animator.StringToHash("IsJumping");
    public readonly int isRunningHash = Animator.StringToHash("IsRunning");

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Set Sensitivity
        followTransform.transform.rotation *= Quaternion.AngleAxis(LookInput.x * aimSensitivity, Vector3.up);
        followTransform.transform.rotation *= Quaternion.AngleAxis(LookInput.y * aimSensitivity, Vector3.left);

        var angles = followTransform.transform.eulerAngles;
        angles.z = 0;

        var angle = followTransform.transform.localEulerAngles.x;

        if (angle > 180 && angle < 300)
        {
            angles.x = 300;
        }
        else if (angle < 180 && angle > 70)
        {
            angles.x = 70;
        }

        //followTransform.transform.localEulerAngles = angles;

        transform.rotation = Quaternion.Euler(0, followTransform.transform.rotation.eulerAngles.y, 0);
        followTransform.transform.localEulerAngles = new Vector3(angles.x, 0, 0);


        if (isJumping) return;

        if (!(InputVector.magnitude > 0)) MoveDirection = Vector3.zero;

        MoveDirection = transform.forward * InputVector.y + transform.right * InputVector.x;
        float currentSpeed = isRunning ? runSpeed : walkSpeed;

        Vector3 movementDirection = MoveDirection * (currentSpeed * Time.deltaTime);

        transform.position += movementDirection;

    }

    public void OnMovement(InputValue val)
    {
        InputVector = val.Get<Vector2>();

        anim.SetFloat(moveXHash, InputVector.x);
        anim.SetFloat(moveYHash, InputVector.y);
    }

    public void OnJump(InputValue val)
    {
        if (isJumping) return;

        anim.SetBool(isJumpingHash, true);
        isJumping = val.isPressed;
        rb.AddForce((transform.up + MoveDirection) * jumpForce, ForceMode.Impulse);
    }

    public void OnRun(InputValue val)
    {
        isRunning = val.isPressed;
        anim.SetBool(isRunningHash, isRunning);
    }

    public void OnLook(InputValue val)
    {
        LookInput = val.Get<Vector2>();
    }

    public void OnAim(InputValue val)
    {
        isAiming = val.isPressed;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground") && !isJumping)
        {
            return;
        }

        isJumping = false;
        anim.SetBool(isJumpingHash, false);
    }

    public void SetSensitivity(float sensitivity)
    {
        aimSensitivity = sensitivity;
    }
}
