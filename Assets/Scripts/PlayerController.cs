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
    public ThirdPersonShooter thirdPersonShooter;
    public static int key;

    //Points
    public static int points;
    int startingPoints = 100;

    //Health
    public float health;
    public float maxHealth = 250;

    bool healthInteractable;
    bool ammoInteractable;
    bool doorInteractable;

    public bool weaponUpgraded;
    bool upgradeInteractable;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        thirdPersonShooter = GetComponent<ThirdPersonShooter>();
    }

    // Start is called before the first frame update
    void Start()
    {
        key = 0;

        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

        crossHair.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        health = maxHealth;
        points = startingPoints;
        weaponUpgraded = false;
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

    public void OnInteract(InputValue value)
    {
        if (healthInteractable && points >= 2500)
        {
            BuyHealth();
        }

        if (ammoInteractable && points >= 2000)
        {
            BuyAmmo();
        }

        if (upgradeInteractable && points >= 5000 && !weaponUpgraded)
        {
            UpgradeWeapon();
        }
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            health -= 7.5f;

            if (health <= 0)
            {
                gameManager.GameOver();
            }
        }

        if (other.gameObject.CompareTag("Door"))
        {
            int cost = other.gameObject.GetComponent<AreaBuyableScript>().Cost;
            doorInteractable = true;

            if (points >= cost && !other.gameObject.GetComponent<AreaBuyableScript>().isOpen)
            {
                points -= cost;
                other.gameObject.GetComponent<AreaBuyableScript>().BuyDoor();
            }
            else if (points < cost && !other.gameObject.GetComponent<AreaBuyableScript>().isOpen)
            {
                gameManager.PopUpText("You need "+cost.ToString()+" Points to buy this door!");
            }
        }

        if (other.gameObject.CompareTag("Portal"))
        {
            if (key >= 5)
            {
                gameManager.WinGame();
            }
            else 
            {
                int keysNeeded = 5 - key;
                gameManager.PopUpText("You need " +keysNeeded.ToString()+ " more keys to unlock Portal.");
            }
        }

        if (other.gameObject.CompareTag("HealthCreate"))
        {
            healthInteractable = true;
            gameManager.PopUpText("Press 'E' to buy Health for "+2500+ " Points.");
        }

        if (other.gameObject.CompareTag("AmmoBox"))
        {
            ammoInteractable = true;
            gameManager.PopUpText("Press 'E' to buy Full Ammo for " + 2000 + " Points.");
        }

        if (other.gameObject.CompareTag("Upgrade") && !weaponUpgraded)
        {
            upgradeInteractable = true;
            gameManager.PopUpText("Press 'E' to Upgrade Weapon for " + 5000 + " Points.");
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        gameManager.PopUpText(" ");

        if (other.gameObject.CompareTag("Door"))
        {
            doorInteractable = false;
        }

        if (other.gameObject.CompareTag("HealthCreate"))
        {
            healthInteractable = false;
        }

        if (other.gameObject.CompareTag("AmmoBox"))
        {
            ammoInteractable = false;
        }

        if (other.gameObject.CompareTag("Upgrade"))
        {
            upgradeInteractable = false;
        }
    }

    public void SetSensitivity(float sensitivity)
    {
        aimSensitivity = sensitivity;
    }

    void BuyHealth()
    {
        health = maxHealth;
        points -= 2500;
    }

    void BuyAmmo()
    {
        thirdPersonShooter.equippedWeapon.weaponStats.totalBullets = 600;
        points -= 2000;
    }

    void UpgradeWeapon()
    {
        thirdPersonShooter.equippedWeapon.weaponStats.clipSize = 100;
        thirdPersonShooter.equippedWeapon.weaponStats.totalBullets = 2000;
        thirdPersonShooter.equippedWeapon.weaponStats.weaponName = "MP115X";
        weaponUpgraded = true;

        points -= 5000;
    }
}
