using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaBuyableScript : MonoBehaviour
{
    public GameObject Door;

    public GameObject[] Spawners;

    public int Cost;
    // Start is called before the first frame update
    void Start()
    {
        Door.SetActive(true);
        for (int i = 0; i < Spawners.Length; i++)
        {
            Spawners[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BuyDoor()
    {
        Door.SetActive(false);
        for (int i = 0; i < Spawners.Length; i++)
        {
            Spawners[i].SetActive(true);
        }
    }
}
