using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcAttackInput : IOrcAttackInput
{
    bool isAttacking;
    bool isInputChanged;

    public bool IsAttacking
    {
        get { return this.isAttacking; }
        set
        {
            if (isAttacking != value)
            {
                this.isAttacking = value;
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
        this.isInputChanged = false;
    }
}
