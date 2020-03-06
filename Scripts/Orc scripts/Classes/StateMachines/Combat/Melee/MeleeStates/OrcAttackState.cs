using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcAttackState : IOrcCombatState
{
    float currentSlashTime;
    float timeBetweenSlash;
    float elapsedTime;
    bool conCurrentCanRun;
    IOrcAttackLogic control;
    EOrcCombatState currentID;
    IInvertoryLogic invertoryLogic;

    Animator animator;
    int isAttackingHash = Animator.StringToHash("IsAttacking");
    int meleeAttackIndexHash = Animator.StringToHash("MeleeAttackIndex");

    public OrcAttackState(bool conCurrentCanRun, ref IOrcAttackLogic control,
        EOrcCombatState stateID, ref IInvertoryLogic invertoryLogic, ref Animator animator)
    {
        this.conCurrentCanRun = conCurrentCanRun;
        this.control = control;
        this.currentID = stateID;
        this.invertoryLogic = invertoryLogic;
        this.animator = animator;
    }

    public IOrcAttackLogic Control { get { return this.control; } }

    public EOrcCombatState StateType { get { return this.currentID; } }

    public bool ConcurrentStateCanRun { get { return this.conCurrentCanRun; } }

    public EOrcCombatState CheckTransition()
    {
        if (elapsedTime > timeBetweenSlash)
        {
            return EOrcCombatState.Idle;
        }
        else if (control.InputInfo.IsAttacking &&
            elapsedTime > currentSlashTime)
        {
            animator.SetBool(isAttackingHash, true);

            elapsedTime = 0;
            invertoryLogic.InputInfo.CurrentWeapons.CurrentMeleeWeapon.WeaponInfo.MeleeProperties.NextSlash();
            animator.SetInteger(meleeAttackIndexHash, invertoryLogic.InputInfo.CurrentWeapons.CurrentMeleeWeapon.Damage());
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
        animator.SetBool(isAttackingHash, true);
        animator.SetInteger(meleeAttackIndexHash, invertoryLogic.InputInfo.CurrentWeapons.CurrentMeleeWeapon.Damage());
        invertoryLogic.InputInfo.CurrentWeapons.CurrentMeleeWeapon.IsMeleeAttackEnabled = true;
        elapsedTime = 0;
        currentSlashTime = invertoryLogic.InputInfo.CurrentWeapons.CurrentMeleeWeapon.WeaponInfo.MeleeProperties.CurrentSlash.DurationTime;
        timeBetweenSlash = invertoryLogic.InputInfo.CurrentWeapons.CurrentMeleeWeapon.WeaponInfo.MeleeProperties.BetweenTimeSlash;
    }

    public void Exit()
    {
        invertoryLogic.InputInfo.CurrentWeapons.CurrentMeleeWeapon.IsMeleeAttackEnabled = false;
        animator.SetBool(isAttackingHash, false);
        invertoryLogic.InputInfo.CurrentWeapons.CurrentMeleeWeapon.WeaponInfo.MeleeProperties.Reset();
    }
 
    public void Update(float time)
    {
        elapsedTime += time;

        if (elapsedTime > currentSlashTime)
        {
            animator.SetBool(isAttackingHash, false);
        }
    }
    //No init implemented
    public void Init()
    {
    }
}
