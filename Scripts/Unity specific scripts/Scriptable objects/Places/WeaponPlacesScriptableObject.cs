using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New weapon placeholder", menuName = "Weapons/Information/Places")]
public class WeaponPlacesScriptableObject : ScriptableObject, IWeaponPlace
{
    [SerializeField]
    Transform idleTransform;
    [SerializeField]
    Transform useTransForm;

    public Transform Idle { get { return this.idleTransform; } }

    public Transform Use { get { return this.useTransForm; } }
}
