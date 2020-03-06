using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnimationInfo : IWeaponAnimation
{
    float equipTime;
    float unEquipTime;

    public float OnEquipTime { get { return this.equipTime; } }

    public float UnequipTime { get { return this.unEquipTime; } }

    public WeaponAnimationInfo(float equipTime, float unEquipTime)
    {
        this.equipTime = equipTime;
        this.unEquipTime = unEquipTime;
    }
}
