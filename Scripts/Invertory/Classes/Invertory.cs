using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invertory : IInvertory
{
    public event Action ChangeEnded;

    bool isChangingWeapon;
    float currentSwitchTime;

    int weaponIndex;
    IEquippedWeapon currentWeapons;
    List<IEquippedWeapon> weapons;
    int currentWeaponNumber;

    public bool IsChangingWeapon { get { return this.isChangingWeapon; } }

    public int SelectedWeaponIndex
    {
        get { return this.weaponIndex; }
        set
        {
            if (value != weaponIndex)
            {
                // For mouse wheel input
                if (value < 0)
                {
                    value = weapons.Count - 1; // return the last weapon in the invertory
                }
                else if (value >= weapons.Count)
                {
                    value = 0; // get the first weapon in the invertory
                }
                weaponIndex = value;
                isChangingWeapon = true;
            }
            else
            {
                isChangingWeapon = false;
            }
        }
    }

    public IEquippedWeapon CurrentWeapons { get { return this.currentWeapons; } }

    public int NumberOfStashes { get { return weapons.Count; } }

    // TODO input elements for the invertory
    public Invertory(List<IEquippedWeapon> weapons)
    {
        this.weaponIndex = 0;
        this.weapons = weapons;
        currentWeapons = new EquippedWeapon();
        currentWeapons.CurrentMeleeWeapon = this.weapons[weaponIndex].CurrentMeleeWeapon;
        currentWeapons.CurrentRangedWeapon = this.weapons[weaponIndex].CurrentRangedWeapon;

        if (currentWeapons.CurrentMeleeWeapon != null)
        {
            currentWeapons.CurrentMeleeWeapon.PlaceWeapon(false);
        }
        if (currentWeapons.CurrentRangedWeapon != null)
        {
            currentWeapons.CurrentRangedWeapon.PlaceWeapon(false);
        }
    }

    public void Equip(IWeapon weapon)
    {
        // TODO equip
    }

    public void IncreaseInvertory()
    {
        // TODO increaseInveretory
    }

    public void SwitchWeapon(bool isRushChange)
    {
        currentWeaponNumber = 0;
        isChangingWeapon = true;
        currentSwitchTime = 0f;

        if (currentWeapons.CurrentMeleeWeapon != null)
        {
            currentWeapons.CurrentMeleeWeapon.PlaceWeapon(true);
        }
        if (currentWeapons.CurrentRangedWeapon != null)
        {
            currentWeapons.CurrentRangedWeapon.PlaceWeapon(true);
        }

        currentWeapons.CurrentRangedWeapon = weapons[weaponIndex].CurrentRangedWeapon;
        currentWeapons.CurrentMeleeWeapon = weapons[weaponIndex].CurrentMeleeWeapon;

        if (currentWeapons.CurrentMeleeWeapon != null)
        {
            currentWeaponNumber++;
            currentSwitchTime = currentWeapons.CurrentMeleeWeapon.EquipAnimationTime.OnEquipTime;
        }
        if (currentWeapons.CurrentRangedWeapon != null && currentWeapons.CurrentRangedWeapon != currentWeapons.CurrentMeleeWeapon)
        {
            currentWeaponNumber++;
            currentSwitchTime = currentSwitchTime > currentWeapons.CurrentRangedWeapon.EquipAnimationTime.OnEquipTime ?
                currentWeapons.CurrentRangedWeapon.EquipAnimationTime.OnEquipTime : currentSwitchTime;
        }

    }

    public void SwitchUpdate(float elapsedTime)
    {
        if (elapsedTime > currentSwitchTime)
        {
            if (currentWeapons.CurrentMeleeWeapon != null && !currentWeapons.CurrentMeleeWeapon.CanUse)
            {
                currentWeapons.CurrentMeleeWeapon.PlaceWeapon(false, elapsedTime);
                if (currentWeapons.CurrentMeleeWeapon.CanUse) currentWeaponNumber--;
            }
            if (currentWeapons.CurrentRangedWeapon != null && !currentWeapons.CurrentRangedWeapon.CanUse)
            {
                currentWeapons.CurrentRangedWeapon.PlaceWeapon(false, elapsedTime);
                if (currentWeapons.CurrentRangedWeapon.CanUse) currentWeaponNumber--;
            }
            if (currentWeaponNumber == 0) // all weapon is at the place
            {
                isChangingWeapon = false;
                if (ChangeEnded != null)
                {
                    ChangeEnded();
                }
            }

        }
    }

    public void SwitchImmidate()
    {
        if (currentWeapons.CurrentMeleeWeapon != null)
        {
            currentWeapons.CurrentMeleeWeapon.PlaceWeapon(false);
        }
        if (currentWeapons.CurrentRangedWeapon != null)
        {
            currentWeapons.CurrentRangedWeapon.PlaceWeapon(false);
        }
        isChangingWeapon = false;
        if (ChangeEnded != null)
        {
            ChangeEnded();
        }
    }

    public void ThrowWeapon(Transform dropPlace)
    {
        // TODO throw weapon
    }

    public void ResupplyWeapons()
    {
        foreach (IEquippedWeapon currentStash in this.weapons)
        {
            if (currentStash.CurrentRangedWeapon != null)
            {
                currentStash.CurrentRangedWeapon.ReFill();
            }
        }
    }
}
