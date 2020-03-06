using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcLifeInfo : IOrcLifeInfo
{
    float currentLife;
    float maxLife;

    public float CurrentLife { get { return this.currentLife; } }

    public float MaximumLife { get { return this.maxLife; } }

    public OrcLifeInfo(float maxLife)
    {
        this.maxLife = maxLife;
        this.currentLife = maxLife;
    }

    public void Damage(float amount)
    {
        if (currentLife > 0)
        {
            this.currentLife -= amount;
            if (currentLife <= 0)
            {
                currentLife = 0;
            }
        }
    }

    public void Reset()
    {
        this.currentLife = maxLife;
    }
}
