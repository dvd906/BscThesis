using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadState : IRangedState
{
    bool conCurrentCanRun;
    IRangedCombatLogic control;
    ERangedAttackID currentID;
    IInvertoryLogic invertoryLogic;
    float reloadTime;

    float elapsedTime;

    Animator animator;
    int isReloadEnabledHash = Animator.StringToHash("IsReloadingEnable");

    // TODO: weapon Invertory current weapon ref InvertoryWeapons
    public ReloadState(bool conCurrentCanRun, ref IRangedCombatLogic control, ERangedAttackID currentID,
        ref IInvertoryLogic invertoryLogic, ref Animator animator)
    {
        this.conCurrentCanRun = conCurrentCanRun;
        this.control = control;
        this.currentID = currentID;
        this.invertoryLogic = invertoryLogic;
        this.animator = animator;
    }

    public bool ConcurrentStateCanRun { get { return this.conCurrentCanRun; } }

    public IRangedCombatLogic Control { get { return this.control; } }

    public ERangedAttackID StateType { get { return this.currentID; } }

    public IInvertoryLogic InvertoryLogic { get { return this.invertoryLogic; } }

    public ERangedAttackID CheckTransition()
    {
        if (elapsedTime > reloadTime)
        {
            return ERangedAttackID.Hold;
        }
        else
        {
            return currentID;
        }
    }

    public void Enter()
    {
        elapsedTime = 0;
        reloadTime = (this.invertoryLogic.InputInfo.CurrentWeapons.CurrentRangedWeapon.WeaponInfo as IRangedWeaponInfo).ReloadTime;
        animator.SetBool(isReloadEnabledHash, true);
    }

    public void Exit()
    {
        invertoryLogic.InputInfo.CurrentWeapons.CurrentRangedWeapon.Reload();
        elapsedTime = 0;
        animator.SetBool(isReloadEnabledHash, false);
    }

    public void Update(float time)
    {
        elapsedTime += time;
    }
    // No init function implmented
    public void Init()
    {
    }
}
