using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpLifeInfo : ISpLifeInfo
{
    public event Action Recharged;

    float rechargeLifeAmount;
    float rechargeTimeFirstAttack;
    float rechargeTime;
    float currentLife;
    float maximumLife;

    float elapsedTime;
    float currentRechargeTime;

    public float RechargeLifeAmount { get { return this.rechargeLifeAmount; } }

    public float RechargeTimeFirstAttack { get { return this.rechargeTimeFirstAttack; } }

    public float RechargeTime { get { return this.rechargeTime; } }

    public float CurrentLife { get { return this.currentLife; } }

    public float MaximumLife { get { return this.maximumLife; } }

    public SpLifeInfo(float rechargeLifeAmount, float rechargeTimeFirstAttack, float rechargeTime, float maximumLife)
    {
        this.rechargeLifeAmount = rechargeLifeAmount;
        this.rechargeTimeFirstAttack = rechargeTimeFirstAttack;
        this.rechargeTime = rechargeTime;
        this.currentLife = maximumLife;
        this.maximumLife = maximumLife;
        this.elapsedTime = 0;
    }

    public void Damage(float amount)
    {
        if (currentLife > 0)
        {
            this.currentLife -= amount;
            currentRechargeTime = rechargeTimeFirstAttack;
            elapsedTime = 0;
            if (currentLife <= 0)
            {
                currentLife = 0;
            }
        }
    }

    public void Recharge(float time)
    {
        elapsedTime += time;
        if (elapsedTime > currentRechargeTime)
        {
            elapsedTime = 0;
            if (currentRechargeTime == rechargeTimeFirstAttack)
            {
                currentRechargeTime = rechargeTime;
            }
            currentLife += rechargeLifeAmount;
            if (currentLife >= maximumLife)
            {
                currentLife = maximumLife;
                if (Recharged != null)
                {
                    Recharged();
                }
            }
        }
    }

    public void ResetCharge()
    {
        elapsedTime = 0;
        currentRechargeTime = rechargeTimeFirstAttack;
    }

    public void Reset()
    {
        this.elapsedTime = 0;
        this.currentLife = maximumLife;
    }
}
