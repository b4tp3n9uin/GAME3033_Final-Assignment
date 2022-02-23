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

    private PlayerController playerController;

    [Header("Sensitivity Values")]
    public float NormalSensitivity = 1;
    public float AimingSensitivity =.5f;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.isAiming)
        {
            aimCam.gameObject.SetActive(true);
            playerController.SetSensitivity(AimingSensitivity);
        }
        else
        {
            aimCam.gameObject.SetActive(false);
            playerController.SetSensitivity(NormalSensitivity);
        }

        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderMask))
        {
            debugTransform.position = raycastHit.point;
        }
    }
}
