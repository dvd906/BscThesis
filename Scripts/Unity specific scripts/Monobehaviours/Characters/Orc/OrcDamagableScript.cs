using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcDamagableScript : MonoBehaviour, IDamagableData
{
    IDamagable orc;

    Animator animator;
    int isDeadHash = Animator.StringToHash("IsDead");

    [SerializeField]
    OrcLifeInfoScriptableObject orcProperties;

    public IDamagable Character { get { return this.orc; } }

    public bool IsEnabled { get { return this.enabled; } }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        IOrcLifeInfo info = new OrcLifeInfo(orcProperties.MaximumLife);
        this.orc = new OrcDamagable(ref info);
        this.orc.Death += Orc_Death;
    }

    private void Orc_Death(int obj)
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
