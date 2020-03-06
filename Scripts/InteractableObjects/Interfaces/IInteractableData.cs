using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractableData : IData
{
    IInteractionLogic InteractionLogic { get; }
}
