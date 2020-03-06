using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractionLogic : IInputLogic<IInteractebleInput>
{
    event Action<IInteractebleInput, IComparable> InputChangedWithId;
    int InteractionID { get; }
}
