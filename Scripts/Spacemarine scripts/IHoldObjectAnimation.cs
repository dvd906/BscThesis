using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHoldObjectAnimator<T>
{
    IHoldObjectAnimator<T> SetupObject(IHoldObjectAnimator<T> information);
    float HoldTime { get; }
    string ParameterToModify { get; }
    int ParameterHash { get; }
    T ValueToModify { get; set; }

}
