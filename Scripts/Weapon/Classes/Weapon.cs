using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : IWeapon
{
    bool canMeleeAttack;
    bool canUse;
    Transform weaponTransform;
    IWeaponAnimation weaponAnimation;
    IWeaponInfo weaponInfo;
    IWeaponPlace weaponPlace;

    public bool CanUse { get { return this.canUse; } }

    public IWeaponAnimation EquipAnimationTime { get { return this.weaponAnimation; } }

    public IWeaponInfo WeaponInfo { get { return this.weaponInfo; } }

    public IWeaponPlace WeaponPlace { get { return this.weaponPlace; } }

    public Transform WeaponTransform { get { return this.weaponTransform; } }

    public bool IsMeleeAttackEnabled
    {
        get { return this.canMeleeAttack; }
        set { this.canMeleeAttack = value; }
    }

    public Weapon() { }

    public Weapon(ref IWeaponAnimation weaponAnimation, ref IWeaponInfo weaponInfo,
        ref IWeaponPlace weaponPlace, Transform weaponTransform)
    {
        this.weaponTransform = weaponTransform;
        this.weaponAnimation = weaponAnimation;
        this.canMeleeAttack = false;
        this.weaponInfo = weaponInfo;
        this.weaponPlace = weaponPlace;
    }

    public void PlaceWeapon(bool isPlaceToIdle, float elapsedTime)
    {
        if (!HasWeaponPlace())
            return;

        if (isPlaceToIdle && elapsedTime > weaponAnimation.UnequipTime)
        {
            weaponTransform.parent = weaponPlace.Idle;
            weaponTransform.position = weaponPlace.Idle.position;
            weaponTransform.rotation = weaponPlace.Idle.rotation;
            canUse = false;
        }
        else if (elapsedTime > weaponAnimation.OnEquipTime)
        {
            weaponTransform.parent = weaponPlace.Use;
            weaponTransform.position = weaponPlace.Use.position;
            weaponTransform.rotation = weaponPlace.Use.rotation;
            canUse = true;
        }

    }

    private bool HasWeaponPlace()
    {
        if (weaponPlace.Idle == null && weaponPlace.Use == null)
        {
            return false;
        }
        return true;
    }

    public void PlaceWeapon(bool isPlaceToIdle)
    {
        if (!HasWeaponPlace())
            return;

        if (isPlaceToIdle)
        {
            weaponTransform.parent = weaponPlace.Idle;
            weaponTransform.position = weaponPlace.Idle.position;
            weaponTransform.rotation = weaponPlace.Idle.rotation;
            canUse = false;
        }
        else
        {
            weaponTransform.parent = weaponPlace.Use;
            weaponTransform.position = weaponPlace.Use.position;
            weaponTransform.rotation = weaponPlace.Use.rotation;
            canUse = true;
        }


    }

    // Returns the current id of the attack for the melee slash
    public int Damage()
    {
        return weaponInfo.MeleeProperties.CurrentSlashIndex;
    }


}
