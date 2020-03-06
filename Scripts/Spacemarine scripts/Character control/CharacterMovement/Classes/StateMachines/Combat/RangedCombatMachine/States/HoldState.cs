using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldState : IRangedState
{
    bool canConcurrentRun;
    IHoldWeapon holdWeaponInfo;
    IRangedCombatLogic control;
    ERangedAttackID currentID;
    ERangedAttackID transitionID;
    IInvertoryLogic invertoryLogic;

    Animator animator;
    int isHoldTimeoutHash = Animator.StringToHash("IsHoldTimeOut");
    int isRaisebackWeaponHash = Animator.StringToHash("IsRaisebackWeapon");
    bool shootAndRaise;

    float elapsedTime;

    public HoldState(bool canConcurrentRun, ref IRangedCombatLogic control, ERangedAttackID currentID,
        ref IHoldWeapon holdWeaponInfo, ref Animator animator, ref IInvertoryLogic invertoryLogic)
    {
        this.canConcurrentRun = canConcurrentRun;
        this.control = control;
        this.currentID = currentID;
        this.animator = animator;
        this.holdWeaponInfo = holdWeaponInfo;
        this.invertoryLogic = invertoryLogic;
    }

    public IRangedCombatLogic Control { get { return this.control; } }

    public ERangedAttackID StateType { get { return this.currentID; } }

    public bool ConcurrentStateCanRun { get { return this.canConcurrentRun; } }

    public IInvertoryLogic InvertoryLogic { get { return null; } }

    public ERangedAttackID CheckTransition()
    {

        if (elapsedTime > holdWeaponInfo.HoldTime)
        {
            animator.SetBool(isHoldTimeoutHash, true);
            transitionID = ERangedAttackID.LayDown;
            shootAndRaise = false;
            return transitionID;
        }
        else if (control.InputInfo.IsAttackingRanged && !control.InputInfo.IsReloadEnabled
            && invertoryLogic.InputInfo.CurrentWeapons.CurrentRangedWeapon.CanShoot)
        {
            transitionID = ERangedAttackID.Shoot;
            shootAndRaise = false;
            return transitionID;
        }
        else if (control.InputInfo.IsReloadEnabled && !control.InputInfo.IsAttackingRanged
            && invertoryLogic.InputInfo.CurrentWeapons.CurrentRangedWeapon.IsReloadEnabled)
        {
            transitionID = ERangedAttackID.Reload;
            return transitionID;
        }
        else
        {
            return this.currentID;
        }
    }

    public void Enter()
    {
        shootAndRaise = true;
        transitionID = this.currentID;
        elapsedTime = 0f;
        animator.SetBool(isHoldTimeoutHash, false);
    }

    public void Exit()
    {
        if (transitionID == currentID)// exit called due to fsm exits
        {
            shootAndRaise = false;
        }
        animator.SetBool(isRaisebackWeaponHash, shootAndRaise);
    }

    public void Update(float time)
    {
        elapsedTime += time;
    }

    // No init function implemented
    public void Init()
    {
    }
}
