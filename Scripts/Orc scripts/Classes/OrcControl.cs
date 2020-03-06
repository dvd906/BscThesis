using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcControl : IOrcControl
{
    IOrcMovementLogic control;
    IOrcAttackLogic meleeLogic;

    IInvertoryLogic invertoryLogic;
    IOrcFSMMovement fSMmovement;
    IOrcFSMCombat fSMmeleeCombat;
    IRotateObject orcRotator;

    public OrcControl(ref IOrcMovementLogic control, ref IMove orcMove, ref Animator animator, ref IOrcAttackLogic orcAttackLogic,
        ref IRotateObject rotator, ref IInvertoryLogic invertoryLogic)
    {
        this.control = control;
        meleeLogic = orcAttackLogic;
        this.invertoryLogic = invertoryLogic;

        this.orcRotator = rotator;
        this.orcRotator.CanRotate = true;

        // Movement FSM
        IOrcMovementState walking = new OrcWalkState(1.2f, true, ref orcMove, EOrcMovementStates.Walking, ref this.control, ref animator);
        IOrcMovementState driveUpState = new OrcDrivenUpState(5, 2, false, ref orcMove, EOrcMovementStates.DrivenUp, ref this.control);
        IOrcMovementState fallState = new OrcFallState(2, Vector3.down, false, ref orcMove, EOrcMovementStates.Falling, ref this.control, ref animator);

        fSMmovement = new OrcFSMMovement(ref this.control);
        fSMmovement.AddState(walking);
        fSMmovement.AddState(driveUpState);
        fSMmovement.AddState(fallState);
        fSMmovement.SetDefaultState(walking);

        // Combat melee
        IOrcCombatState idleState = new OrcMeleeIdle(true, ref orcAttackLogic, EOrcCombatState.Idle);
        IOrcCombatState attackState = new OrcAttackState(false, ref orcAttackLogic, EOrcCombatState.Attacking, ref invertoryLogic, ref animator);

        fSMmeleeCombat = new OrcFSMCombat(ref orcAttackLogic);
        fSMmeleeCombat.AddState(idleState);
        fSMmeleeCombat.AddState(attackState);
        fSMmeleeCombat.SetDefaultState(idleState);

        if (this.invertoryLogic.HasInputModel)
            this.invertoryLogic.InputInfo.SelectedWeaponIndex = 0;
    }

    public IOrcMovementLogic Control { get { return this.control; } }

    public void Update(float time)
    {
        fSMmovement.UpdateMachine(time);
        if (control.InputInfo.Targeter.HasTarget)
        {
            orcRotator.RotateObject(control.InputInfo.Targeter.Target);
            fSMmeleeCombat.UpdateMachine(time);
        }
    }

    public void UpdatePerceptions(float time)
    {
        control.ReadInput();
        if(invertoryLogic.HasInputModel)
            meleeLogic.ReadInput();
    }

    // No init implemented
    public void Init()
    {
    }
}
