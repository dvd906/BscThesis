using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRangedWeaponData : IWeaponData
{
    IRangedWeapon RangedWeapon { get; }
}
