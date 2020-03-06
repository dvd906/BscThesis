using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldPistolOld : IHoldPistolAnimation
{
    private float currentTime;

    public float HoldTime { get; private set; }

    public string ParameterToModify { get; private set; }

    public bool ValueToModify { get; set; }

    public int ParameterHash { get; private set; }

    public bool IsHoldingPistol { get; set; }

    public float HoldPistolTimeSeconds { get; set; }

    public IHoldObjectAnimator<bool> SetupObject(IHoldObjectAnimator<bool> information)
    {
        this.HoldTime = information.HoldTime;
        this.ParameterToModify = this.ParameterToModify;
        this.ValueToModify = information.ValueToModify;
        this.ParameterHash = information.ParameterHash;
        return this;
    }

    public void Enter(Animator stateObject)
    {
        currentTime = 0.0f;
    }

    public void Update(Animator stateObject)
    {

        currentTime += Time.deltaTime;
        if (currentTime > HoldTime)
        {
            stateObject.SetBool(ParameterHash, true);
        }
    }

    public void Exit(Animator stateObject)
    {
        stateObject.SetBool(ParameterHash, false);
    }

    public HoldPistolOld()
    {

    }

    public HoldPistolOld(float holdTime, string parameterToModify, bool isHoldingPistolStart)
    {
        this.HoldTime = holdTime;
        this.ParameterToModify = parameterToModify;
        this.IsHoldingPistol = isHoldingPistolStart;
    }
}
