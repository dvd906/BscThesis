using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpDamagable : IDamagable
{
    public event Action<int> Death;

    bool isAlive;

    ISpShieldData shieldData;
    ISpLifeData spLifeData;

    public SpDamagable(ref ISpShieldData shieldData, ref ISpLifeData spLifeData)
    {
        this.shieldData = shieldData;
        this.spLifeData = spLifeData;
        this.isAlive = true;
        this.shieldData.Shield.Recharged += Shield_Recharged;
        this.spLifeData.Life.Recharged += Life_Recharged;
        this.shieldData.EnableComponent(false);
        this.spLifeData.EnableComponent(false);
        this.Death += SpDamagable_Death;
    }

    // Somewhere else
    private void SpDamagable_Death(int obj)
    {
        this.spLifeData.EnableComponent(false);
        this.spLifeData.EnableComponent(false);
    }

    public bool IsAlive { get { return this.isAlive; } }

    public void TakeDamage(float amount)
    {
        if (!isAlive)
        {
            return;
        }
        if (!shieldData.IsEnabled)
        {
            shieldData.EnableComponent(true);
        }
        if (!spLifeData.IsEnabled && shieldData.Shield.CurrentShieldStatus == 0)
        {
            spLifeData.EnableComponent(true);
        }

        if (shieldData.Shield.CurrentShieldStatus > 0)
        {
            shieldData.Shield.Damage(amount);
            spLifeData.Life.ResetCharge();
        }
        else if (spLifeData.IsEnabled && spLifeData.Life.CurrentLife > 0)
        {
            shieldData.Shield.ResetCharge();
            spLifeData.Life.Damage(amount);
        }
        else
        {
            isAlive = false;
            if (Death != null)
            {
                Death(0);
            }
        }

    }

    private void Life_Recharged()
    {
        this.spLifeData.EnableComponent(false);
    }

    private void Shield_Recharged()
    {
        this.shieldData.EnableComponent(false);
    }

    public void Reset()
    {
        isAlive = true;
        this.shieldData.Shield.Reset();
        this.spLifeData.Life.Reset();
    }
}
