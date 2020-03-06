using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpShieldInfo : ISpShieldInfo
{
    public event Action Recharged;

    float rechargeShieldAmount;
    float rechargeTimeFirstAttack;
    float rechargeTime;
    float currentShield;
    float maximumShield;

    float elapsedTime;
    float currentRechargeTime;

    public float RechargeShieldAmount { get { return this.rechargeShieldAmount; } }

    public float RechargeTimeFirstAttack { get { return this.rechargeTimeFirstAttack; } }

    public float RechargeTime { get { return this.rechargeTime; } }

    public float CurrentShieldStatus { get { return this.currentShield; } }

    public float MaximumShield { get { return this.maximumShield; } }

    public SpShieldInfo(float rechargeShieldAmount, float rechargeTimeFirstAttack, float rechargeTime, float maximumShield)
    {
        this.rechargeShieldAmount = rechargeShieldAmount;
        this.rechargeTimeFirstAttack = rechargeTimeFirstAttack;
        this.rechargeTime = rechargeTime;
        this.currentShield = maximumShield;
        this.maximumShield = maximumShield;
        elapsedTime = 0;
    }

    public void Damage(float amount)
    {
        if (currentShield > 0)
        {
            this.currentShield -= amount;
            currentRechargeTime = rechargeTimeFirstAttack;
            elapsedTime = 0;
            if (currentShield <= 0)
            {
                currentShield = 0;
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
            currentShield += rechargeShieldAmount;
            if (currentShield >= maximumShield)
            {
                currentShield = maximumShield;
                if (Recharged != null)
                {
                    Recharged();
                }
            }
        }
    }

    public void ResetCharge()
    {
        this.currentRechargeTime = rechargeTimeFirstAttack;
        elapsedTime = 0;
    }

    public void Reset()
    {
        this.currentShield = maximumShield;
    }
}
