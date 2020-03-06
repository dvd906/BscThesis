using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(InvertoryScript))]
[RequireComponent(typeof(NavMeshAgent))]
public class OrcMovementScript : MonoBehaviour, IMovementData
{
    [SerializeField]
    Animator animator;

    [SerializeField]
    LayerMask environment;

    NavMeshAgent meshAgent;

    IOrcAIInput orcAIInput;
    IMove orcMove;
    IRotateObject orcAIRotator;

    IOrcControl orcControl;
    IInvertoryLogic invertoryLogic;

    ITarget AITargeter;
    ICommandableData commandableData;

    public bool IsEnabled { get { return this.enabled; } }

    public ITarget Targeter { get { return this.AITargeter; } }

    public void EnableComponent(bool isEnabled)
    {
        this.enabled = isEnabled;
        meshAgent.enabled = isEnabled;
    }

    private void Start()
    {
        meshAgent = GetComponent<NavMeshAgent>();
        IInvertory invertory = GetComponent<IInvertoryData>().Invertory;
        invertoryLogic = new InvertoryLogic(ref invertory);

        CharacterController characterController = GetComponent<CharacterController>();
        orcAIRotator = new AIOrcRotator(this.transform, 5);
        orcMove = new AIOrcMove(this.transform, environment, 2f, characterController, meshAgent);

        AITargeter = new AITarget(this.transform);
        IOrcMovementInput orcMovementInput = new OrcMovementInput(ref AITargeter);
        orcMovementInput.DriveUpDirection = Vector3.forward;
        IOrcMovementLogic orcMovementLogic = new AIOrcMovementLogic(orcMovementInput, meshAgent, this.transform);

        IAIAttackInput orcAttackInput = new AIOrcAttackInput(AITargeter);
        IOrcAttackLogic orcAttackLogic = new AIOrcAttackLogic(this.transform, ref orcAttackInput, 2.0f, ref orcMovementLogic);

        orcAIInput = new AIOrcInput(orcMovementLogic, orcAttackLogic, meshAgent);

        orcAIRotator.CanRotate = true;
        orcControl = new OrcControl(ref orcMovementLogic, ref orcMove, ref animator, ref orcAttackLogic, ref orcAIRotator, ref invertoryLogic);

        commandableData = GetComponent<ICommandableData>();
        if (commandableData != null)
        {
            commandableData.CommandableUnit = new CommandableUnit(AITargeter);
            commandableData.EnableComponent(true);
        }
    }

    private void Update()
    {
        orcControl.UpdatePerceptions(Time.deltaTime);
        orcControl.Update(Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!orcControl.Control.InputInfo.Targeter.HasTarget &&
            !Physics.Raycast(transform.position + (Vector3.up / 2), other.gameObject.transform.position + (Vector3.up / 2), environment))
        {
            Debug.DrawLine(transform.position + (Vector3.up / 2), other.gameObject.transform.position + (Vector3.up / 2), Color.red);
            orcControl.Control.InputInfo.Targeter.Target = other.gameObject.transform;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!orcControl.Control.InputInfo.Targeter.HasTarget &&
            !Physics.Raycast(transform.position + (Vector3.up / 2), other.gameObject.transform.position + (Vector3.up / 2), environment))
        {
            orcControl.Control.InputInfo.Targeter.Target = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //if (orcControl.Control.InputInfo.Targeter.HasTarget &&
        //    Physics.Raycast(transform.position + (Vector3.up / 2), other.gameObject.transform.position + (Vector3.up / 2), environment))
        //{
        //    orcControl.Control.InputInfo.Targeter.Target = null; // lost the target
        //    return;
        //}
        //if (orcControl.Control.InputInfo.Targeter.Target == null &&
        //    !Physics.Raycast(transform.position + (Vector3.up / 2), other.gameObject.transform.position + (Vector3.up / 2), environment))
        //{
        //    Debug.DrawLine(transform.position + (Vector3.up / 2), other.gameObject.transform.position + (Vector3.up / 2), Color.red);
        //    orcControl.Control.InputInfo.Targeter.Target = other.gameObject.transform;
        //}
    }
}

