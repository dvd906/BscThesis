using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandableScript : MonoBehaviour, ICommandableData
{
    public ICommandable CommandableUnit { get; set; }

    public bool IsEnabled { get { return enabled; } }

    [SerializeField]
    MovableUnitScriptableObject behaviour;

    void Start()
    {
        this.enabled = false;   
    }

    void OnEnable()
    {
        IMovementData movementData = GetComponent<IMovementData>();
        CommandableUnit.MovementBehaviour = behaviour.StartMovementBehavior;
        CommandableUnit.PreferredAttackType = behaviour.StartAttackType;
    }

    public void EnableComponent(bool isEnabled)
    {
        enabled = isEnabled;
    }
}
