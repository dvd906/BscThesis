using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvertorySwitch : IInvertoryState
{
    bool conCurrentCanRun;

    float elapsedTime;
    IInvertoryLogic control;

    Animator animator;
    int isMeleeEquippedHash = Animator.StringToHash("IsMeleeWeaponsEquipped");
    int isSwitchingEnabledHash = Animator.StringToHash("IsSwitchingEnabled");

    float lerpFloat;
    float weight;
    bool isMeleeEquipment;

    public IInvertoryLogic Control { get { return this.control; } }

    public EInvertoryStateID StateType { get { return EInvertoryStateID.Switching; } }

    public bool ConcurrentStateCanRun { get { return this.conCurrentCanRun; } }

    public InvertorySwitch(bool conCurrentCanRun, ref IInvertoryLogic control, ref Animator animator)
    {
        this.conCurrentCanRun = conCurrentCanRun;
        this.control = control;
        this.animator = animator;
    }

    public EInvertoryStateID CheckTransition()
    {
        if (!control.InputInfo.IsChangingWeapon)
        {
            return EInvertoryStateID.None;
        }
        else
        {
            return EInvertoryStateID.Switching;
        }
    }

    public void Enter()
    {
        lerpFloat = 0f;
        weight = 1f;
        elapsedTime = 0f;
        control.InputInfo.SwitchWeapon(false);
        isMeleeEquipment = control.InputInfo.CurrentWeapons.IsMeleeEquipment;
        animator.SetBool(isSwitchingEnabledHash, true);
        animator.SetBool(isMeleeEquippedHash, isMeleeEquipment);
    }

    public void Exit()
    {
        if (control.InputInfo.IsChangingWeapon)
        {
            control.InputInfo.SwitchImmidate();
        }
        animator.SetBool(isSwitchingEnabledHash, false);
    }

    public void Update(float time)
    {
        elapsedTime += time;
        SetLayerWeights();
        control.InputInfo.SwitchUpdate(elapsedTime);

    }

    private void SetLayerWeights()
    {
        if (lerpFloat > 1)
        {
            return;
        }
        lerpFloat += 0.2f;
        weight = 1 - lerpFloat;
        if (isMeleeEquipment)
        {
            //Ranged layers
            animator.SetLayerWeight(2, weight);
            animator.SetLayerWeight(4, weight);
            animator.SetLayerWeight(6, weight);
            animator.SetLayerWeight(8, weight);
            animator.SetLayerWeight(11, weight);
            //Melee layers
            animator.SetLayerWeight(1, lerpFloat);
            animator.SetLayerWeight(3, lerpFloat);
            animator.SetLayerWeight(5, lerpFloat);
            animator.SetLayerWeight(7, lerpFloat);
            animator.SetLayerWeight(10, lerpFloat);
        }
        else
        {
            //Ranged layers
            animator.SetLayerWeight(2, lerpFloat);
            animator.SetLayerWeight(4, lerpFloat);
            animator.SetLayerWeight(6, lerpFloat);
            animator.SetLayerWeight(8, lerpFloat);
            animator.SetLayerWeight(11, lerpFloat);
            //Melee layers
            animator.SetLayerWeight(1, weight);
            animator.SetLayerWeight(3, weight);
            animator.SetLayerWeight(5, weight);
            animator.SetLayerWeight(7, weight);
            animator.SetLayerWeight(10, weight);
        }
    }

    // No init implemented
    public void Init()
    {
    }
}
