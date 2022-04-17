using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieAIScript : MonoBehaviour
{
    NavMeshAgent navAgent;
    public GameObject target;
    float zombieHealth;
    public bool isAlive;

    // Start is called before the first frame update
    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();

        isAlive = true;
        zombieHealth = 100;

        target = GameObject.FindGameObjectWithTag("Player");

        Destroy(gameObject, 200);
    }

    // Update is called once per frame
    void Update()
    {

        navAgent.SetDestination(target.transform.position);
    }

    public void DamageZombie(int damageBy)
    {
        zombieHealth -= damageBy;
        PlayerController.points += 20;

        if (zombieHealth <= 0)
        {
            FindObjectOfType<AudioManager>().Play("ZombieDeath");
            PlayerController.points += 100;
            isAlive = false;
            navAgent.speed = 0;
            Destroy(gameObject, 4);
        }
            
    }
}
