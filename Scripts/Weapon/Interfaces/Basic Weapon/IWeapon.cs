using System;
using UnityEngine;

// Describes one weapon in the running game
public interface IWeapon
{
    bool IsMeleeAttackEnabled { get; set; }
    bool CanUse { get; }
    Transform WeaponTransform { get; }
    IWeaponAnimation EquipAnimationTime { get; }
    IWeaponInfo WeaponInfo { get; }
    IWeaponPlace WeaponPlace { get; }
    void PlaceWeapon(bool isPlaceToIdle);
    void PlaceWeapon(bool isPlaceToIdle, float elapsedTime);
    int Damage();

}
