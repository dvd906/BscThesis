using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableInput : IInteractebleInput
{
    bool isInteracting;
    bool isInputChanged;

    public bool IsInteracting
    {
        get { return this.isInteracting; }

        set
        {
            if (value != isInteracting)
            {
                this.isInteracting = value;
                if (!isInputChanged)
                {
                    isInputChanged = true;
                }
            }
        }
    }
    public bool IsInputChanged { get { return this.isInputChanged; } }

    public InteractableInput()
    {
        this.isInteracting = false;
        this.isInputChanged = false;
    }

    public void ResetChange()
    {
        this.isInputChanged = false;
    }
}
