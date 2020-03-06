using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class InvertoryScript : MonoBehaviour, IInvertoryData
{
    [SerializeField]
    InvertoryStructureScriptableObject invertoryStructure;

    IInvertory invertory;
    IInvertoryLogic invertoryLogic;

    public IInvertory Invertory { get { return invertory; } }

    public bool IsEnabled { get; private set; }

    public IInvertoryLogic InvertoryLogic { get { return this.invertoryLogic; } }

    public void EnableComponent(bool isEnabled)
    {
        this.enabled = isEnabled;
        this.IsEnabled = isEnabled;
    }

    private void Start()
    {
        IWeaponData[] weaponsData = GetComponentsInChildren<IWeaponData>();
        if (weaponsData.Length != 0)
        {
            List<IEquippedWeapon> invertoryItems = CreateInvertory(invertoryStructure.StashStructure, weaponsData.ToList());
            invertory = new Invertory(invertoryItems);
            invertoryLogic = new InvertoryLogic(ref invertory);
        }
        else
        {
            invertoryLogic = new InvertoryLogic();
        }
    }

    private List<IEquippedWeapon> CreateInvertory(List<InvertoryLayoutScriptableObject> stashStructure, List<IWeaponData> weaponsData)
    {
        List<IEquippedWeapon> invertory = new List<IEquippedWeapon>();

        foreach (IInvertoryOneStashLayout oneStash in stashStructure)
        {
            IEquippedWeapon stash = new EquippedWeapon();
            if (oneStash.MeleeWeaponType != EWeaponType.Empty)
            {
                stash.CurrentMeleeWeapon = SearchItem(oneStash.MeleeWeaponType, ref weaponsData);
            }
            if (oneStash.RangedWeaponType != EWeaponType.Empty)
            {
                stash.CurrentRangedWeapon = (IRangedWeapon)SearchItem(oneStash.RangedWeaponType, ref weaponsData);
                if (stash.CurrentMeleeWeapon == null)
                {
                    stash.CurrentMeleeWeapon = stash.CurrentRangedWeapon;
                }
            }
            invertory.Add(stash);
        }

        return invertory;
    }

    private IWeapon SearchItem(EWeaponType weaponType, ref List<IWeaponData> weaponsData)
    {
        var searched = from currentData in weaponsData
                       where currentData.Weapon.WeaponInfo.WeaponType == weaponType
                       select currentData;

        if (searched != null && searched.Count() >= 1)
        {
            IWeaponData data = searched.FirstOrDefault();
            weaponsData.Remove(data);
            return data.Weapon;
        }
        else
        {
            throw new Exception("Invertory structure is incorrect!");
        }
    }
}
