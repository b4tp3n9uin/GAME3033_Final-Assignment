using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : MonoBehaviour
{
    private Rigidbody bullet_rb;

    [SerializeField]
    private Transform blood; 

    private void Awake()
    {
        bullet_rb = GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    void Start()
    {
        float speed = 40;
        bullet_rb.velocity = transform.forward * speed;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Zombie"))
        {
            Instantiate(blood, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}
