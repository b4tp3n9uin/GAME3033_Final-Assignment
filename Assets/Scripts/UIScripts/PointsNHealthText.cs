using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsNHealthText : MonoBehaviour
{
    [SerializeField]
    TMPro.TextMeshProUGUI HealthText;

    [SerializeField]
    TMPro.TextMeshProUGUI PointsText;

    public PlayerController playerController;

    private void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerController)
            return;

        HealthText.text = "Health: " + playerController.health.ToString() + 
            " / " + playerController.maxHealth.ToString();

        PointsText.text = "Points: " + PlayerController.points.ToString();
    }
}
