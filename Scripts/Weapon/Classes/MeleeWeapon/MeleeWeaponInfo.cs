using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeaponInfo : IWeaponInfo
{
    ISlashLogic meleeProperties;

    public EWeaponType WeaponType { get { return EWeaponType.Melee; } }

    public bool IsMeleeWeapon { get { return true; } }

    public ISlashLogic MeleeProperties { get { return this.meleeProperties; } }

    public string WeaponName { get; private set; }

    public MeleeWeaponInfo(ISlashLogic meleeProperties, string weaponName)
    {
        this.meleeProperties = meleeProperties;
        WeaponName = weaponName;
    }
}
