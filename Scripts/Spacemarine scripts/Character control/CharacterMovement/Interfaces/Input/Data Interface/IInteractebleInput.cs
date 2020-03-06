using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractebleInput : IInput
{
    bool IsInteracting { get; set; }
}
