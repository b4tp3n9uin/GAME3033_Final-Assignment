using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    MP5, Shotgun, AK47, M16, M60
}

public enum FirePattern
{
    SemiAuto, FullAuto, ThreeBurst
}

[System.Serializable]
public struct WeaponStats
{
    public WeaponType weaponType;
    public FirePattern firingPattern;
    public string weaponName;
    public float damage;
    public int bulletsInClip;
    public int clipSize;
    public float firestartDelay;
    public float fireRate;
    public bool reapeating;
    public int totalBullets;
}

public class WeaponComponent : MonoBehaviour
{
    public Transform GripLocation;
    public WeaponStats weaponStats;

    bool isFiring;
    bool isReloading;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void StartFiringWeapon()
    {
        isFiring = true;

        if (weaponStats.reapeating)
        {
            //fire weapon
        }
    }

    public virtual void StopFiringWeapon()
    {

    }

    public virtual void FireWeapon()
    {
        weaponStats.bulletsInClip--;
    }

    public virtual void ReloadWeapon()
    {
        int remainingBullets = weaponStats.clipSize - weaponStats.bulletsInClip;

        if (weaponStats.totalBullets < weaponStats.clipSize)
        {
            weaponStats.bulletsInClip = weaponStats.totalBullets;
            weaponStats.totalBullets = 0;
        }
        else
        {
            weaponStats.totalBullets -= remainingBullets;
            weaponStats.bulletsInClip = weaponStats.clipSize;
        }
    }

    public virtual void StartReloading()
    {
        isReloading = true;
        ReloadWeapon();
    }

    public virtual void StopReloading()
    {
        isReloading = false;
    }
}
