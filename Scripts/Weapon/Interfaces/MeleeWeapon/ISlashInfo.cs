using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// One melee slash property
public interface ISlashInfo
{
    float DurationTime { get; }
    float Damage { get; }

    void MeleeAttack();
}
