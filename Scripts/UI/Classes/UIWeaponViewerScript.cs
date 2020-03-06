using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class UIWeaponViewerScript : MonoBehaviour
{
    [SerializeField]
    string ammoSeparator;
    [SerializeField]
    string concatenationStyle;
    [SerializeField]
    Text weaponNameHolder;
    [SerializeField]
    Text ammoHolder;

    bool isDataAvialable = false;
    IInvertory invertory;
    StringBuilder builder;

    void Start()
    {
        invertory = GetComponent<IInvertoryData>().Invertory;
        isDataAvialable = invertory != null && weaponNameHolder != null && ammoHolder != null && invertory.NumberOfStashes != 0;
        if (isDataAvialable)
        {
            builder = new StringBuilder();
            UpdateWeaponInfo(invertory.CurrentWeapons);
        }
    }

    private void Update()
    {
        if (builder == null)
            return;

        if (invertory.IsChangingWeapon)
            UpdateWeaponInfo(invertory.CurrentWeapons);
        else
            UpdateRangedWeaponAmmoText(invertory.CurrentWeapons);
    }

    private void UpdateRangedWeaponAmmoText(IEquippedWeapon currentWeapons)
    {
        if (currentWeapons.CurrentRangedWeapon != null && builder != null)
        {
            builder.Remove(0, builder.ToString().Length);
            ammoHolder.text = CurrentAmmoOutputStyle
                (currentWeapons.CurrentRangedWeapon.WeaponInfo.CurrentAmmoInMagazine,
                 ammoSeparator,
                 currentWeapons.CurrentRangedWeapon.WeaponInfo.CurrentAmmoMax);
        }
    }

    private string CurrentAmmoOutputStyle(int currentAmmoInMagazine, string separator, int currentAmmoMax)
    {
        builder.Append(currentAmmoInMagazine);
        builder.Append(separator);
        builder.Append(currentAmmoMax);
        return builder.ToString();
    }

    private void UpdateWeaponInfo(IEquippedWeapon currentWeapons)
    {
        builder.Remove(0, builder.ToString().Length);
        if (currentWeapons.IsMeleeEquipment)
        {
            builder.Append(currentWeapons.CurrentMeleeWeapon.WeaponInfo.WeaponName);

            if (currentWeapons.CurrentRangedWeapon != null)
            {
                builder.Append(concatenationStyle);
                builder.Append(currentWeapons.CurrentRangedWeapon.WeaponInfo.WeaponName);
                weaponNameHolder.text = builder.ToString();
                builder.Remove(0, builder.ToString().Length);

                ammoHolder.text = CurrentAmmoOutputStyle
                (currentWeapons.CurrentRangedWeapon.WeaponInfo.CurrentAmmoInMagazine,
                 ammoSeparator,
                 currentWeapons.CurrentRangedWeapon.WeaponInfo.CurrentAmmoMax);
            }
            else
            {
                weaponNameHolder.text = builder.ToString();
            }
        }
        else
        {
            weaponNameHolder.text = currentWeapons.CurrentRangedWeapon.WeaponInfo.WeaponName;
            ammoHolder.text = CurrentAmmoOutputStyle
                (currentWeapons.CurrentRangedWeapon.WeaponInfo.CurrentAmmoInMagazine,
                 ammoSeparator,
                 currentWeapons.CurrentRangedWeapon.WeaponInfo.CurrentAmmoMax);
        }
    }
}
