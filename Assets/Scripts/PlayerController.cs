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
    public bool isShooting;
    public bool isReloading;

    [Header("Locomotion")]
    public float walkSpeed = 5;
    public float runSpeed = 8;
    public float jumpForce = 5;

    private float aimSensitivity = 1;

    public Animator anim;
    Rigidbody rb;
    public GameObject followTransform;
    public GameObject crossHair;

    Vector2 InputVector = Vector2.zero;
    Vector3 MoveDirection = Vector3.zero;
    Vector2 LookInput = Vector2.zero;
    
    public readonly int moveXHash = Animator.StringToHash("MovementX");
    public readonly int moveYHash = Animator.StringToHash("MovementY");
    public readonly int isJumpingHash = Animator.StringToHash("IsJumping");
    public readonly int isRunningHash = Animator.StringToHash("IsRunning");
    public readonly int isReloadingHash = Animator.StringToHash("isReloading");

    public GameManager gameManager;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

        crossHair.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        gameManager.isPaused = false;
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

        if (!isAiming)
        {
            anim.SetBool(isJumpingHash, true);
            isJumping = val.isPressed;
            rb.AddForce((transform.up + MoveDirection) * jumpForce, ForceMode.Impulse);
        }
    }

    public void OnRun(InputValue val)
    {
        if (!isAiming)
        {
            isRunning = val.isPressed;
            anim.SetBool(isRunningHash, isRunning);
        }
    }

    public void OnLook(InputValue val)
    {
        if (!gameManager.isPaused)
            LookInput = val.Get<Vector2>();
    }

    public void OnAim(InputValue val)
    {
        if (!isRunning && !isJumping && !isReloading)
        {
            isAiming = val.isPressed;
            crossHair.SetActive(isAiming);
        }
    }

    public void OnShoot(InputValue val)
    {
        if (isAiming)
            isShooting = val.isPressed;
    }

    public void OnReload(InputValue val)
    {
        if (!isAiming)
        {
            isReloading = val.isPressed;
            anim.SetBool(isReloadingHash, true);
        }
    }

    public void OnPause(InputValue value)
    {
        gameManager.PausePressed();
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
