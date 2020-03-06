using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface ILifeInfo
{
    float CurrentLife { get; }
    float MaximumLife { get; }

    void Damage(float amount);
    void Reset();
}
