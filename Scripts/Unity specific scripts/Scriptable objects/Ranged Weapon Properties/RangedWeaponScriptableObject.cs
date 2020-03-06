using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New basic weapon properties", menuName = "Weapons/Information/Ranged/ Ranged weapon properties")]
public class RangedWeaponScriptableObject : BasicWeaponInfoScriptableObject
{
    [SerializeField]
    int damage;
    [SerializeField]
    int numberOfAmmoInMagazine;
    [SerializeField]
    int maximumMagazineSize;
    [SerializeField]
    float reloadTime;
    [SerializeField]
    float shootDurationTime;
    [SerializeField]
    LayerMask damagableLayer;

    public int Damage { get { return damage; } }

    public int NumberOfAmmoInMagazine { get { return numberOfAmmoInMagazine; } }

    public int MaximumMagazineSize { get { return maximumMagazineSize; } }

    public float ReloadTime { get { return reloadTime; } }

    public float ShootDurationTime { get { return shootDurationTime; } }

    public LayerMask DamagableLayer { get { return damagableLayer; } }
}
