using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterMovementScript : MonoBehaviour, IMovementData
{
    [SerializeField]
    Animator characterAnimator;
    [SerializeField]
    LayerMask collideWith;
    [SerializeField]
    Transform rayCastFrom;
    [SerializeField]
    CameraFollowScript userCamera;

    IMove characterControl;

    IMovementAndMeleeCombatInput movementInput;
    IMovementAndMeleeCombatLogic movementLogic;

    IRangedCombatInput rangedInput;
    IRangedCombatLogic rangedLogic;

    IControl<IMovementAndMeleeCombatLogic> spaceMove;

    IInvertoryLogic invertoryLogic;
    ISpCameraFollow spCamera;

    public bool IsEnabled { get { return this.enabled; } }

    public ITarget Targeter { get { return null; } }

    public void EnableComponent(bool isEnabled)
    {
        this.enabled = isEnabled;
        spCamera.ObjectRotator.CanRotate = false;
    }

    // Use this for initialization
    void Start()
    {
        ICameraData camData = userCamera.GetComponent<ICameraData>();
        spCamera = camData.SpCamera;

        invertoryLogic = GetComponent<IInvertoryData>().InvertoryLogic;

        Transform follow = transform;
        CharacterController characterController = GetComponent<CharacterController>();
        characterControl = new SpacemarineMove(ref characterController, ref follow, collideWith);

        movementInput = new SpMovementAndMeleeCombat(ref rayCastFrom, collideWith, ref characterController);
        movementLogic = new SpMovementAndMeleeLogic(movementInput);

        rangedInput = new RangedCombatInput();
        rangedLogic = new RangedCombatLogic(ref rangedInput, spCamera.CameraTransform);

        spaceMove = new ControlSpMovement(ref movementLogic, ref characterControl,
                                          ref characterAnimator, ref rangedLogic,
                                          ref invertoryLogic, ref spCamera);
    }


    void Update()
    {
        spaceMove.UpdatePerceptions(Time.deltaTime);
        spaceMove.Update(Time.deltaTime);
    }
}
