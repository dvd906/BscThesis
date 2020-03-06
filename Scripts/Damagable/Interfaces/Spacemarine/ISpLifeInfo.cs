using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpLifeInfo : ILifeInfo
{
    event Action Recharged;

    //TODO Proeprties interface, for the intarface and the scriptable objects
    float RechargeLifeAmount { get; }
    float RechargeTimeFirstAttack { get; }
    float RechargeTime { get; }

    void Recharge(float time);
    void ResetCharge();
}
