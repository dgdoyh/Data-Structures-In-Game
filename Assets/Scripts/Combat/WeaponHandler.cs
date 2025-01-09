using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This will enable/disable weapon
public class WeaponHandler : MonoBehaviour
{
    // This will contains weapon manager
    [SerializeField] private GameObject weapon;

    // Animation Event
    public void EnableWeapon()
    {
        weapon.SetActive(true);
    }

    // Animation Event
    public void DisableWeapon()
    {
        weapon.SetActive(false);
    }
}
