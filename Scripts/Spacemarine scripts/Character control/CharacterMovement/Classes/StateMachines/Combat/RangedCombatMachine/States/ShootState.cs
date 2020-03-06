using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootState : IRangedShootState
{
    bool canConcurentRun;
    ERangedAttackID currentStateID;
    IRangedCombatLogic control;
    IInvertoryLogic invertoryLogic;

    Animator animator;
    int isAttackRangedHash = Animator.StringToHash("IsAttackingRanged");
    int IsRaisebackWeaponHash = Animator.StringToHash("IsRaisebackWeapon");

    float elapsedTime;
    float shootTime;

    public IRangedCombatLogic Control { get { return this.control; } }

    public ERangedAttackID StateType { get { return this.currentStateID; } }

    public bool ConcurrentStateCanRun { get { return this.canConcurentRun; } }

    public IInvertoryLogic InvertoryLogic { get { return this.invertoryLogic; } }

    public float CurrentShootDuration { get; set; }

    public ShootState(bool canConcurentRun, ERangedAttackID currentStateID, ref IRangedCombatLogic control,
        ref Animator animator, ref IInvertoryLogic invertoryLogic)
    {
        this.canConcurentRun = canConcurentRun;
        this.currentStateID = currentStateID;
        this.control = control;
        this.animator = animator;
        this.invertoryLogic = invertoryLogic;
    }

    public ERangedAttackID CheckTransition()
    {
        if (elapsedTime > shootTime)
        {
            return ERangedAttackID.Hold;
        }
        else
        {
            return this.currentStateID;
        }
    }

    public void Enter()
    {
        elapsedTime = 0;
        animator.SetBool(isAttackRangedHash, true);
        shootTime = invertoryLogic.InputInfo.CurrentWeapons.CurrentRangedWeapon.WeaponInfo.ShootDurationTime;
        invertoryLogic.InputInfo.CurrentWeapons.CurrentRangedWeapon.Shoot(control.CameraTransform);
    }

    public void Exit()
    {
        animator.SetBool(isAttackRangedHash, false);
        animator.SetBool(IsRaisebackWeaponHash, true);
    }

    public void Update(float time)
    {
        elapsedTime += time;
    }

    // No init function needed
    public void Init()
    {
    }

}
