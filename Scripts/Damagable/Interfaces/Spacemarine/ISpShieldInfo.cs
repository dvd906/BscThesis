using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpShieldInfo : IShieldInfo
{
    event Action Recharged;

    float RechargeShieldAmount { get; }
    float RechargeTimeFirstAttack { get; }
    float RechargeTime { get; }

    void Recharge(float time);
    void ResetCharge();
}
