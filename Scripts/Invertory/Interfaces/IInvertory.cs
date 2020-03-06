using UnityEngine;
using System;

public interface IInvertory
{
    event Action ChangeEnded;
    bool IsChangingWeapon { get; }
    int NumberOfStashes { get; }
    int SelectedWeaponIndex { get; set; }
    IEquippedWeapon CurrentWeapons { get; }

    void Equip(IWeapon weapon);
    void ThrowWeapon(Transform dropPlace); // TODO not a transform for Throw the weapon down
    void SwitchWeapon(bool isRushChange);
    void SwitchUpdate(float elapsedTime); // when will be ready
    void SwitchImmidate();
    void IncreaseInvertory(); // Later you can equip other weapons
    void ResupplyWeapons();
}
