using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpShieldInfoScript : MonoBehaviour, ISpShieldData
{
    [SerializeField]
    SpShieldScriptableObject shieldData;

    ISpShieldInfo shieldInfo;

    public ISpShieldInfo Shield { get { return this.shieldInfo; } }

    public bool IsEnabled { get { return this.enabled; } }

    private void Awake()
    {
        shieldInfo = new SpShieldInfo(shieldData.RechargeShieldAmount, shieldData.RechargeTimeFirstAttack,
            shieldData.RechargeTime, shieldData.MaximumShield);
    }

    private void Update()
    {
        this.shieldInfo.Recharge(Time.deltaTime);
    }

    public void EnableComponent(bool isEnabled)
    {
        this.enabled = isEnabled;
    }
}
