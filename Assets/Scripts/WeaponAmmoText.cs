using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponAmmoText : MonoBehaviour
{
    [SerializeField]
    TMPro.TextMeshProUGUI weaponNameText;

    [SerializeField]
    TMPro.TextMeshProUGUI ammoCountText;

    [SerializeField]
    WeaponComponent weaponComponent;

    /// <summary>
    /// Set up events for onweaponequipped to handel weapon we grab.
    /// </summary>

    private void OnEnable()
    {
        PlayerEvents.OnWeaponEquipped += OnWeaponEquipped;
    }

    private void OnDisable()
    {
        PlayerEvents.OnWeaponEquipped += OnWeaponEquipped;
    }

    void OnWeaponEquipped(WeaponComponent _weaponComponent)
    {
        weaponComponent = _weaponComponent;
    }

    // Update is called once per frame
    void Update()
    {
        if (!weaponComponent)
            return;

        weaponNameText.text = weaponComponent.weaponStats.weaponName;
        ammoCountText.text = weaponComponent.weaponStats.bulletsInClip.ToString() +
            " / " + weaponComponent.weaponStats.totalBullets.ToString();
    }
}
