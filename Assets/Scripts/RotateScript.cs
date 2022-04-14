using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateScript : MonoBehaviour
{
    public GameObject Model;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Model.transform.Rotate(0, 90 * Time.deltaTime, 0, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerController.key++;
            Debug.Log("Keys: " + PlayerController.key);
            Destroy(gameObject);
        }
    }
}
