using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    event Action<int> Death;
    bool IsAlive { get; }
    void TakeDamage(float amount);
    void Reset();
}
