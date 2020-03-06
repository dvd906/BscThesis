using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New shield info", menuName = "Characters/Spacemarine Properties/Sp Shield Info")]
public class SpShieldScriptableObject : ScriptableObject
{
    [SerializeField]
    float rechargeShieldAmount;
    [SerializeField]
    float rechargeTimeFirstAttack;
    [SerializeField]
    float rechargeTime;
    [SerializeField]
    float maximumShield;

    public float RechargeShieldAmount { get { return rechargeShieldAmount; } }

    public float RechargeTimeFirstAttack { get { return rechargeTimeFirstAttack; } }

    public float RechargeTime { get { return rechargeTime; } }

    public float MaximumShield { get { return maximumShield; } }
}
