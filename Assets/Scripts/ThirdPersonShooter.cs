using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

public class ThirdPersonShooter : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera aimCam;
    [SerializeField]
    private LayerMask aimColliderMask = new LayerMask();
    [SerializeField]
    private Transform debugTransform;
    [SerializeField]
    private Transform ofBulletPrefab;
    [SerializeField]
    private Transform muzzle;

    private PlayerController playerController;
    private Animator anim;

    [Header("Sensitivity Values")]
    public float NormalSensitivity = 1;
    public float AimingSensitivity =.5f;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<PlayerController>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mouseWorldPosition = Vector3.zero;

        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderMask))
        {
            debugTransform.position = raycastHit.point;
            mouseWorldPosition = raycastHit.point;
        }

        if (playerController.isAiming)
        {
            aimCam.gameObject.SetActive(true);
            playerController.SetSensitivity(AimingSensitivity);
            anim.SetLayerWeight(1, Mathf.Lerp(anim.GetLayerWeight(1), 0.7f, Time.deltaTime * 10f));
            
        }
        else
        {
            aimCam.gameObject.SetActive(false);
            playerController.SetSensitivity(NormalSensitivity);
            anim.SetLayerWeight(1, Mathf.Lerp(anim.GetLayerWeight(1), 0f, Time.deltaTime * 10f));
        }

        if (playerController.isShooting)
        {
            Vector3 aimDir = (mouseWorldPosition - muzzle.position).normalized;
            Instantiate(ofBulletPrefab, muzzle.position, Quaternion.LookRotation(aimDir, Vector3.up));
            playerController.isShooting = false;
        }
    }
}
