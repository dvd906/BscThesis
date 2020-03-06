using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedCombatLogic : IRangedCombatLogic
{
    Transform cameraTransform;

    public IRangedCombatInput InputInfo { get; private set; }

    public Transform CameraTransform { get { return this.cameraTransform; } }

    public bool HasInputModel { get { return this.InputInfo != null; } }

    public event Action<IRangedCombatInput> InputChanged;

    public RangedCombatLogic(ref IRangedCombatInput combatInput, Transform cameraTransform)
    {
        this.InputInfo = combatInput;
        this.cameraTransform = cameraTransform;
    }

    public void ReadInput()
    {
        if (!HasInputModel)
            return;

        this.InputInfo.ResetChange();

        InputInfo.IsAttackingRanged = Input.GetButton("Fire1");
        InputInfo.IsReloadEnabled = Input.GetButtonDown("Reload");
        InputInfo.IsZoomingEnabled = Input.GetButton("Zoom");

        if (InputChanged != null && InputInfo.IsInputChanged)
        {
            InputChanged(this.InputInfo);
        }
    }

    public void Reset()
    {
        InputInfo.IsAttackingRanged = false;
        InputInfo.IsReloadEnabled = false;
    }
}
