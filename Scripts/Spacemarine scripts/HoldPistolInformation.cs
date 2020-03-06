using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu()]
public class HoldPistolInformation : ScriptableObject, IHoldPistolAnimationInfo
{
    [SerializeField]
    string parameterToModify;
    [SerializeField]
    float holdTime;

    public float HoldPistolTimeSeconds { get; set; }

    public bool IsHoldingPistol { get; set; }

    public float HoldTime { get { return holdTime; } }

    public string ParameterToModify { get { return parameterToModify; } }

    public int ParameterHash { get {return Animator.StringToHash(parameterToModify); } }

    public bool ValueToModify { get; set; }

    public IHoldObjectAnimator<bool> SetupObject(IHoldObjectAnimator<bool> information)
    {
        throw new System.NotImplementedException();
    }
}
