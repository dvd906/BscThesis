using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashInfo : ISlashInfo
{
    float slashDurationTime;
    float damage;
    
    public float DurationTime { get { return this.slashDurationTime; } }

    public float Damage { get { return this.damage; } }

    public SlashInfo(float slashDurationTime, float damage)
    {
        this.slashDurationTime = slashDurationTime;
        this.damage = damage;
    }

    public void MeleeAttack()
    {
        // TODO: melee attack slashinfo
    }
}
