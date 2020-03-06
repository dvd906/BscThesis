using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEquippedWeapon
{
    bool IsMeleeEquipment { get; }
    IWeapon CurrentMeleeWeapon { get; set; }
    IRangedWeapon CurrentRangedWeapon { get; set; }
}
