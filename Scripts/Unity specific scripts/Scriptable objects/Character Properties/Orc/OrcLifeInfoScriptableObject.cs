using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New orc life property", menuName = "Characters/Orc Life properties")]
public class OrcLifeInfoScriptableObject : ScriptableObject, IOrcLifeInfo
{
    [SerializeField]
    float maxLife;
    float currentLife;

    public float CurrentLife { get { return this.currentLife; } }

    public float MaximumLife { get { return this.maxLife; } }

    public void Damage(float amount)
    {
    }

    public void Reset()
    {
    }

    private void OnEnable()
    {
        this.currentLife = maxLife;
    }
}
