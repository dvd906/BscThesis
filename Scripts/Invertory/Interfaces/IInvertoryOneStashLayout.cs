using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Shows the structure of the invertory layout
public interface IInvertoryOneStashLayout
{
    EWeaponType MeleeWeaponType { get; }
    EWeaponType RangedWeaponType { get; }
}
