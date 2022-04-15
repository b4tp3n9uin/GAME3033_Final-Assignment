using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaBuyableScript : MonoBehaviour
{
    public GameObject Door;

    public GameObject[] Spawners;

    public int Cost;

    public bool isOpen;
    // Start is called before the first frame update
    void Start()
    {
        isOpen = false;

        Door.SetActive(true);
        for (int i = 0; i < Spawners.Length; i++)
        {
            Spawners[i].SetActive(false);
        }
    }

    public void BuyDoor()
    {
        isOpen = true;

        Door.SetActive(false);
        for (int i = 0; i < Spawners.Length; i++)
        {
            Spawners[i].SetActive(true);
        }
    }
}
