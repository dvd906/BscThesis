using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Shows the built up of one stash
[CreateAssetMenu(fileName ="New invertory stash", menuName = "Invertory/One stash")]
public class InvertoryLayoutScriptableObject : ScriptableObject, IInvertoryOneStashLayout
{
    [SerializeField]
    EWeaponType meleeWeaponType;
    [SerializeField]
    EWeaponType rangedWeaponType;

    public EWeaponType MeleeWeaponType { get { return this.meleeWeaponType; } }

    public EWeaponType RangedWeaponType { get { return this.rangedWeaponType; } }
}
