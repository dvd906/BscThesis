using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcDamagable : IDamagable
{
    public event Action<int> Death;

    IOrcLifeInfo orc;
    bool isAlive;

    public bool IsAlive { get { return this.isAlive; } }

    public OrcDamagable(ref IOrcLifeInfo orc)
    {
        this.orc = orc;
        this.isAlive = true;
    }

    public void TakeDamage(float amount)
    {
        if (!isAlive)
        {
            return;
        }
        this.orc.Damage(amount);
        if (this.orc.CurrentLife <= 0)
        {
            isAlive = false;
            if (Death != null)
            {
                Death(0);// TODO send the id of the object
            }
        }
    }

    public void Reset()
    {

    }
}
