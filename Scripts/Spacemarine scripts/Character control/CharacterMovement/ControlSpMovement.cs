using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class ControlSpMovement : IControl<IMovementAndMeleeCombatLogic>
{
    const bool CAN_ROTATE = true;

    IMovementAndMeleeCombatLogic movementAndMeleeControl;
    ISpCameraFollow spCamera;

    IFSMMovementSpacemarine fSMSpMovement;
    IFSMCombatSpaceMarine fSMSpCombat;
    IFSMRangedSpacemarine fSMSpRangedCombat;
    IFSMInvertory fSMInvertory;

    IRangedCombatLogic rangedInput;
    IInvertoryLogic invertoryInput;

    public IMovementAndMeleeCombatLogic Control { get { return this.movementAndMeleeControl; } }

    public ControlSpMovement(ref IMovementAndMeleeCombatLogic input, ref IMove characterController
        , ref Animator animator, ref IRangedCombatLogic rangedLogic, ref IInvertoryLogic invertoryLogic, ref ISpCameraFollow camera)
    {

        this.rangedInput = rangedLogic;
        this.rangedInput.InputChanged += RangedInput_InputChanged;
        this.invertoryInput = invertoryLogic;
        this.spCamera = camera;

        // Movement FSM setup
        this.movementAndMeleeControl = input;
        this.fSMSpMovement = new FSMachineSpacemarineMove(EMovementID.Walking, ref this.movementAndMeleeControl, true);

        WalkState walkState = new WalkState(true, 2.0f, ref characterController, ref this.movementAndMeleeControl, EMovementID.Walking, ref animator);
        RunState runState = new RunState(false, 5.0f, ref characterController, ref this.movementAndMeleeControl, EMovementID.Running, ref animator);
        SmashState smashState = new SmashState(false, 6.0f, 1.25f, ref characterController, ref this.movementAndMeleeControl, EMovementID.Smashing, ref animator);
        FallState fallState = new FallState(2.0f, -Physics.gravity.y * Vector3.down, true, ref characterController, ref this.movementAndMeleeControl, EMovementID.Falling, ref animator);
        LandState landState = new LandState(0.75f, false, ref input, ref characterController, EMovementID.Landing);

        this.fSMSpMovement.AddState(walkState);
        this.fSMSpMovement.AddState(runState);
        this.fSMSpMovement.AddState(smashState);
        this.fSMSpMovement.AddState(fallState);
        this.fSMSpMovement.AddState(landState);
        this.fSMSpMovement.SetDefaultState(walkState);

        this.fSMSpMovement.StateChanged += FSMSpMovement_StateChanged;

        // Melee FSM machine setup
        AttackState attackState = new AttackState(false, EMeleeAttackID.Attacking, ref this.movementAndMeleeControl, ref invertoryLogic, ref animator);
        MeleeIdle meleeIdle = new MeleeIdle(true, EMeleeAttackID.Idle, ref this.movementAndMeleeControl);

        this.fSMSpCombat = new FSMCombatSpacemarine(true, EMeleeAttackID.Idle, ref this.movementAndMeleeControl, ref invertoryLogic);

        this.fSMSpCombat.AddState(attackState);
        this.fSMSpCombat.AddState(meleeIdle);
        this.fSMSpCombat.SetDefaultState(meleeIdle);

        //Ranged FSM setup
        IHoldWeapon holdWeaponInfo = new HoldPistol(5.0f);
        IRangedState holdState = new HoldState(true, ref rangedLogic, ERangedAttackID.Hold, ref holdWeaponInfo, ref animator, ref invertoryInput);
        IRangedShootState shootState = new ShootState(true, ERangedAttackID.Shoot, ref rangedLogic, ref animator, ref invertoryLogic);
        IRangedState reloadState = new ReloadState(true, ref rangedLogic, ERangedAttackID.Reload, ref invertoryLogic, ref animator);
        IRangedState laydownState = new LaydownState(true, ref rangedLogic, ERangedAttackID.LayDown, ref invertoryLogic);

        this.fSMSpRangedCombat = new FSMRangedSpacemarine(true, ERangedAttackID.LayDown, ref rangedLogic, ref invertoryLogic);

        this.fSMSpRangedCombat.AddState(holdState);
        this.fSMSpRangedCombat.AddState(reloadState);
        this.fSMSpRangedCombat.AddState(shootState);
        this.fSMSpRangedCombat.AddState(laydownState);
        this.fSMSpRangedCombat.SetDefaultState(laydownState);

        this.fSMSpRangedCombat.StateChanged += FSMSpRangedCombat_StateChanged;

        // Invertory Setup
        InvertoryIdle invertoryIdleState = new InvertoryIdle(true, ref invertoryLogic);
        InvertorySwitch invertorySwitchState = new InvertorySwitch(true, ref invertoryLogic, ref animator);

        this.fSMInvertory = new FSMInvertory(EInvertoryStateID.None, ref invertoryLogic);

        this.fSMInvertory.AddState(invertoryIdleState);
        this.fSMInvertory.AddState(invertorySwitchState);
        this.fSMInvertory.SetDefaultState(invertoryIdleState);

        this.fSMInvertory.StateChanged += FSMInvertory_StateChanged;

        if (invertoryLogic.HasInputModel)
        {
            this.spCamera.ObjectRotator.CanRotate = !invertoryLogic.InputInfo.CurrentWeapons.IsMeleeEquipment;
            this.spCamera.IsMeleeViewEnabled = !invertoryLogic.InputInfo.CurrentWeapons.IsMeleeEquipment;
        }
        else
        {
            this.spCamera.IsMeleeViewEnabled = false;
        }
    }

    public void Update(float time)
    {
        fSMSpMovement.UpdateMachine(time);

        if (fSMSpMovement.ConcurrentStateCanRun)
        {
            fSMSpCombat.UpdateMachine(time);
            fSMSpRangedCombat.UpdateMachine(time);
            fSMInvertory.UpdateMachine(time);
        }
    }

    public void UpdatePerceptions(float time)
    {
        movementAndMeleeControl.ReadInput();
        if (fSMSpMovement.ConcurrentStateCanRun)
        {
            invertoryInput.ReadInput();
            rangedInput.ReadInput();
        }
    }
    //Init  function not needed
    public void Init()
    {
    }

    private void RangedInput_InputChanged(IRangedCombatInput obj)
    {
        if (obj.IsZoomingEnabled != spCamera.IsZooming)
        {
            spCamera.SetZoom(obj.IsZoomingEnabled);
            if (!spCamera.IsZooming && fSMSpMovement.StateType == EMovementID.Idle
                && fSMSpRangedCombat.StateType == ERangedAttackID.LayDown)
            {
                spCamera.ObjectRotator.CanRotate = this.invertoryInput.HasInputModel ?
                    !this.invertoryInput.InputInfo.CurrentWeapons.IsMeleeEquipment :
                    CAN_ROTATE;
            }
            else
            {
                spCamera.ObjectRotator.CanRotate = true;
            }
        }
    }

    private void FSMInvertory_StateChanged()
    {
        spCamera.IsMeleeViewEnabled = invertoryInput.InputInfo.CurrentWeapons.IsMeleeEquipment;
        spCamera.SetPosition(fSMSpMovement.StateType);
        if (!invertoryInput.InputInfo.CurrentWeapons.IsMeleeEquipment)
        {
            spCamera.ObjectRotator.CanRotate = true;
        }
        else
        {
            spCamera.ObjectRotator.CanRotate = !(EMovementID.Idle == fSMSpMovement.StateType);
            fSMSpRangedCombat.Exit();
        }
    }

    private void FSMSpRangedCombat_StateChanged()
    {
        if (!invertoryInput.HasInputModel)
            return;

        if (!invertoryInput.InputInfo.CurrentWeapons.IsMeleeEquipment)
        {
            spCamera.ObjectRotator.CanRotate = true;
            return;
        }
        if (fSMSpRangedCombat.StateType == ERangedAttackID.LayDown && fSMSpMovement.StateType == EMovementID.Idle)
        {
            spCamera.ObjectRotator.CanRotate = false;
        }
        else
        {
            spCamera.ObjectRotator.CanRotate = true;
        }
    }

    private void FSMSpMovement_StateChanged()
    {
        if (!fSMSpMovement.ConcurrentStateCanRun)
        {
            fSMSpCombat.Reset();
            fSMSpRangedCombat.Reset();
            fSMInvertory.Reset();
        }

        switch (fSMSpMovement.StateType)
        {
            case EMovementID.Idle:
                spCamera.ObjectRotator.CanRotate = invertoryInput.HasInputModel ?
                    !invertoryInput.InputInfo.CurrentWeapons.IsMeleeEquipment ||
                    rangedInput.InputInfo.IsZoomingEnabled || fSMSpRangedCombat.StateType == ERangedAttackID.Hold :
                    true;
                break;
            case EMovementID.Walking:
                spCamera.ObjectRotator.CanRotate = true;
                break;
            case EMovementID.Running:
                spCamera.ObjectRotator.CanRotate = true;
                break;
            case EMovementID.Smashing:
                spCamera.ObjectRotator.CanRotate = false;
                break;
        }
        spCamera.SetPosition(fSMSpMovement.StateType);
    }
}
