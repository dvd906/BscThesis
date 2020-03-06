using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedCombatInput : IRangedCombatInput
{
    bool isAttackingRanged;
    bool isReloadEnabled;
    bool isInputChanged;
    bool isZoomingEnabled;

    public bool IsAttackingRanged
    {
        get { return this.isAttackingRanged; }
        set
        {
            if (isAttackingRanged != value)
            {
                isAttackingRanged = value;
                if (!isInputChanged)
                {
                    isInputChanged = true;
                }
            }
        }
    }

    public bool IsReloadEnabled
    {
        get { return this.isReloadEnabled; }
        set
        {
            if (isReloadEnabled && isAttackingRanged)
            {
                isReloadEnabled = false;
                if (!isInputChanged)
                {
                    isInputChanged = true;
                }
            }
            else if (isReloadEnabled != value)
            {
                isReloadEnabled = value;
                if (!isInputChanged)
                {
                    isInputChanged = true;
                }
            }
        }
    }

    public bool IsInputChanged { get { return this.isInputChanged; } }

    public bool IsZoomingEnabled
    {
        get { return this.isZoomingEnabled; }
        set
        {
            if (value != isZoomingEnabled)
            {
                isZoomingEnabled = value;
                if (!isInputChanged)
                {
                    isInputChanged = true;
                }
            }
        }
    }

    public RangedCombatInput()
    {
        this.isInputChanged = false;
        this.isReloadEnabled = false;
    }

    public void ResetChange()
    {
        this.isInputChanged = false;
    }
}
