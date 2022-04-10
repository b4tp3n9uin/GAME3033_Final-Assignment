using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieAIScript : MonoBehaviour
{
    NavMeshAgent navAgent;
    public Transform target;
    float zombieHealth;

    // Start is called before the first frame update
    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();

        zombieHealth = 100;
    }

    // Update is called once per frame
    void Update()
    {
        navAgent.SetDestination(target.position);
    }

    public void DamageZombie(int damageBy)
    {
        zombieHealth -= damageBy;

        if (zombieHealth <= 0)
            Destroy(gameObject);
    }
}
