using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// IShieldinfo same as ILifeInfo, but later development might change it
public interface IShieldInfo
{
    float CurrentShieldStatus { get; }
    float MaximumShield { get; }

    void Damage(float amount);
    void Reset();
}
