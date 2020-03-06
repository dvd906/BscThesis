using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIOrcAttackInput : IAIAttackInput
{
    bool isInputChanged;
    bool isAttacking;
    ITarget target;

    public AIOrcAttackInput(ITarget target)
    {
        this.target = target;
    }

    public ITarget Targeter { get { return this.target; } }

    public bool IsAttacking
    {
        get { return this.isAttacking; }
        set
        {
            if (isAttacking != value)
            {
                isAttacking = value;
                if (!isInputChanged)
                {
                    isInputChanged = true;
                }
            }
        }
    }

    public bool IsInputChanged { get { return this.isInputChanged; } }

    public void ResetChange()
    {
        isAttacking = false;
        isInputChanged = true;
    }
}
