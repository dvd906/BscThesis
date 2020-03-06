using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionLogic : IInteractionLogic
{
    public IInteractebleInput InputInfo { get; private set; }
    public event Action<IInteractebleInput, IComparable> InputChangedWithId;
    public event Action<IInteractebleInput> InputChanged;

    public int InteractionID { get; private set; }

    public bool HasInputModel { get { return InputInfo != null; } }

    public InteractionLogic(ref IInteractebleInput input, int interactionID)
    {
        this.InputInfo = input;
        InteractionID = interactionID;
    }

    public void ReadInput()
    {
        if (HasInputModel)
        {
            InputInfo.IsInteracting = Input.GetButtonDown("Interact");

            if (InputInfo.IsInputChanged)
            {
                if (InputChangedWithId != null)
                    InputChangedWithId(InputInfo, InteractionID);
                if (InputChanged != null)
                    InputChanged(InputInfo);
                Reset();
            }
        }
    }

    public void Reset()
    {
        InputInfo.ResetChange();
    }
}
