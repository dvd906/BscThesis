using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpDamagableScript : MonoBehaviour, IDamagableData
{
    IDamagable spacemarine;

    Animator animator;
    int isDeadHash = Animator.StringToHash("IsDead");

    public IDamagable Character { get { return this.spacemarine; } }

    public bool IsEnabled { get { return this.enabled; } }

    private void OnEnable()
    {
        ISpLifeData spLifeData = GetComponent<ISpLifeData>();
        ISpShieldData shieldData = GetComponent<ISpShieldData>();
        spacemarine = new SpDamagable(ref shieldData, ref spLifeData);
        spacemarine.Death += Spacemarine_Death;
        animator = GetComponent<Animator>();
    }

    private void Spacemarine_Death(int obj)
    {
        if (animator != null)
        {
            animator.SetBool(isDeadHash, true);
        }
    }

    public void EnableComponent(bool isEnabled)
    {
        this.enabled = isEnabled;
    }
}
