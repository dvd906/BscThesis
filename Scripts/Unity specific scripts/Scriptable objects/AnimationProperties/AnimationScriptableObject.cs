using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Animation equip timing", menuName = "Weapons/Information/Animation equip time")]
public class AnimationScriptableObject : ScriptableObject, IWeaponAnimation
{
    [SerializeField]
    float onEquipTime;
    [SerializeField]
    float unEquipTime;

    public float OnEquipTime { get { return this.onEquipTime; } }

    public float UnequipTime { get { return this.unEquipTime; } }
}
