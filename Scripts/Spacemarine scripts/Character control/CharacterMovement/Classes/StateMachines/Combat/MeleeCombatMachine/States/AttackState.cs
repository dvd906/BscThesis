using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IMeleeState
{
    // TODO add slaslogic list for easy handling
    float currentSlashTime;
    float timeBetweenSlash;
    float elapsedTime;
    bool concurrentCanRun;
    Animator animator;
    EMeleeAttackID currentID;
    IMovementAndMeleeCombatLogic control;
    ISlashLogic slashes;

    int isMovingHash = Animator.StringToHash("IsMoving");
    int indexSlashHash = Animator.StringToHash("Slashes");
    int isAttackingMeleeHash = Animator.StringToHash("IsAttackingMelee");

    IInvertoryLogic invertoryLogic;

    public IMovementAndMeleeCombatLogic Control { get { return this.control; } }

    public EMeleeAttackID StateType { get { return this.currentID; } }

    public bool ConcurrentStateCanRun { get { return this.concurrentCanRun; } }

    public AttackState(bool concurrentCanRun, EMeleeAttackID currentID, ref IMovementAndMeleeCombatLogic control, ref IInvertoryLogic invertory, ref Animator animator)
    {
        this.concurrentCanRun = concurrentCanRun;
        this.currentID = currentID;
        this.control = control;
        this.animator = animator;
        this.invertoryLogic = invertory;
    }

    public EMeleeAttackID CheckTransition()
    {
        if (elapsedTime > timeBetweenSlash || control.InputInfo.IsRunning || control.InputInfo.IsFalling)
        {
            return EMeleeAttackID.Idle;
        }
        else if (control.InputInfo.IsAttackingMelee &&
            elapsedTime > currentSlashTime)
        {
            animator.SetBool(isAttackingMeleeHash, true);

            elapsedTime = 0;
            invertoryLogic.InputInfo.CurrentWeapons.CurrentMeleeWeapon.WeaponInfo.MeleeProperties.NextSlash();
            animator.SetInteger(indexSlashHash, invertoryLogic.InputInfo.CurrentWeapons.CurrentMeleeWeapon.Damage());
            currentSlashTime = invertoryLogic.InputInfo.CurrentWeapons.CurrentMeleeWeapon.WeaponInfo.MeleeProperties.CurrentSlash.DurationTime;
            timeBetweenSlash = invertoryLogic.InputInfo.CurrentWeapons.CurrentMeleeWeapon.WeaponInfo.MeleeProperties.BetweenTimeSlash;
            return this.currentID;
        }
        else
        {
            return this.currentID;
        }

    }

    public void Enter()
    {
        animator.SetBool(isAttackingMeleeHash, true);
        animator.SetInteger(indexSlashHash, invertoryLogic.InputInfo.CurrentWeapons.CurrentMeleeWeapon.Damage());
        invertoryLogic.InputInfo.CurrentWeapons.CurrentMeleeWeapon.IsMeleeAttackEnabled = true;
        elapsedTime = 0;
        currentSlashTime = invertoryLogic.InputInfo.CurrentWeapons.CurrentMeleeWeapon.WeaponInfo.MeleeProperties.CurrentSlash.DurationTime;
        timeBetweenSlash = invertoryLogic.InputInfo.CurrentWeapons.CurrentMeleeWeapon.WeaponInfo.MeleeProperties.BetweenTimeSlash;
    }

    public void Exit()
    {
        invertoryLogic.InputInfo.CurrentWeapons.CurrentMeleeWeapon.IsMeleeAttackEnabled = false;
        animator.SetBool(isAttackingMeleeHash, false);
        invertoryLogic.InputInfo.CurrentWeapons.CurrentMeleeWeapon.WeaponInfo.MeleeProperties.Reset();
    }

    public void Init()
    {
    }

    public void Update(float time)
    {
        elapsedTime += time;

        if (elapsedTime > currentSlashTime)
        {
            animator.SetBool(isAttackingMeleeHash, false);
        }
    }
}
