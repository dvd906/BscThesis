using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInput
{
    bool IsInputChanged { get; }
    void ResetChange();
}
