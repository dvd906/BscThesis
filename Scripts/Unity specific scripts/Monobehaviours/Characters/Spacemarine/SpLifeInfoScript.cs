using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpLifeInfoScript : MonoBehaviour, ISpLifeData
{
    //TODO Scriptable property
    [SerializeField]
    SpLifeScriptableObject lifeData;

    ISpLifeInfo spLifeInfo;

    public ISpLifeInfo Life { get { return this.spLifeInfo; } }

    public bool IsEnabled { get { return this.enabled; } }

    private void Awake()
    {
        this.spLifeInfo = new SpLifeInfo(lifeData.RechargeLifeAmount, lifeData.RechargeTimeFirstAttack,
            lifeData.RechargeTime, lifeData.MaximumLife);
    }

    private void Update()
    {
        this.spLifeInfo.Recharge(Time.deltaTime);
    }

    public void EnableComponent(bool isEnabled)
    {
        this.enabled = isEnabled;
    }
}
