using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeaponAnimation
{
    float OnEquipTime { get; }
    float UnequipTime { get; }
}
