using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject zombie;
    public bool stopSpawing;
    public float SpawnTime;
    public float spawnDelay;

    // Start is called before the first frame update
    void Start()
    {
        stopSpawing = false;

        InvokeRepeating("SpawnZombie", SpawnTime, spawnDelay);
    }

    public void SpawnZombie()
    {
        Instantiate(zombie, transform.position, transform.rotation);
        if (stopSpawing)
        {
            // Do Something
            CancelInvoke("SpawnZOmbie");
        }
    }
}
