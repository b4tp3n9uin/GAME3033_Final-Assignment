using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ThirdPersonShooter : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera aimCam;

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
    }
}
