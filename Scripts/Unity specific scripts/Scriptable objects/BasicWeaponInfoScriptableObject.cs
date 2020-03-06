using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New basic weapon", menuName = "Weapons/Information/Basic Weapon description")]
public class BasicWeaponInfoScriptableObject : ScriptableObject
{
    [SerializeField]
    string weaponName;
    [SerializeField]
    EWeaponType weaponType;
    [SerializeField]
    bool isMelee;

    public string WeaponName { get { return weaponName; } }

    public EWeaponType WeaponType { get { return weaponType; } }
}
