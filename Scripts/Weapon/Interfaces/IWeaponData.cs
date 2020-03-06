using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeaponData : IData
{
    IWeapon Weapon { get; }
}
