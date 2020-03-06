using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Every weapon can be used as melee
// All weapons has this property
public interface IWeaponInfo
{
    string WeaponName { get; }
    EWeaponType WeaponType { get; }
    bool IsMeleeWeapon { get; }
    ISlashLogic MeleeProperties { get; }
}
