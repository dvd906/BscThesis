using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New life info", menuName = "Characters/Spacemarine Properties/Sp life properties")]
public class SpLifeScriptableObject : ScriptableObject
{
    [SerializeField]
    float rechargeLifeAmount;
    [SerializeField]
    float rechargeTimeFirstAttack;
    [SerializeField]
    float rechargeTime;
    [SerializeField]
    float maximumLife;

    public float RechargeLifeAmount { get { return rechargeLifeAmount; } }

    public float RechargeTimeFirstAttack { get { return rechargeTimeFirstAttack; } }

    public float RechargeTime { get { return rechargeTime; } }

    public float MaximumLife { get { return maximumLife; } }
}
