using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquippedWeapon : IEquippedWeapon
{
    bool isMeleeEquipment;
    IWeapon meleeWeapon;
    IRangedWeapon rangedWeapon;

    public IWeapon CurrentMeleeWeapon
    {
        get { return meleeWeapon; }
        set
        {
            this.meleeWeapon = value;
            isMeleeEquipment = this.meleeWeapon == null ? false : this.meleeWeapon.WeaponInfo.IsMeleeWeapon;
        }
    }

    public IRangedWeapon CurrentRangedWeapon
    {
        get { return this.rangedWeapon; }
        set { this.rangedWeapon = value; }
    }

    public bool IsMeleeEquipment { get { return isMeleeEquipment; } }

    public EquippedWeapon()
    {
        this.rangedWeapon = null;
        this.meleeWeapon = null;
    }

    // Only ranged or melee weapon
    public EquippedWeapon(IWeapon weapon)
    {
        meleeWeapon = weapon;
        rangedWeapon = (weapon is IRangedWeapon) ? (IRangedWeapon)weapon : null;
    }

    // If you have melee and ranged weapon
    public EquippedWeapon(IWeapon meleeWeapon, IWeapon rangedWeapon)
    {
        this.meleeWeapon = meleeWeapon;
        this.rangedWeapon = (rangedWeapon is IRangedWeapon) ? (IRangedWeapon)rangedWeapon : null;
        if (this.rangedWeapon == null)
        {
            throw new System.Exception("Not a ranged weapon added");
        }
    }
}
