using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyText : MonoBehaviour
{

    [SerializeField]
    TMPro.TextMeshProUGUI keyText;

    // Update is called once per frame
    void Update()
    {
        keyText.text = "Keys: " + PlayerController.key.ToString() + " / 5";
    }
}
