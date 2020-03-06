using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandableUnit : ICommandable
{
    public CommandableUnit(EAttackType startAttacktype, EStanceType startMovementBehaviour, ITarget targeter)
    {
        PreferredAttackType = startAttacktype;
        MovementBehaviour = startMovementBehaviour;
        Targeter = targeter;
    }

    public CommandableUnit( ITarget targeter)
    {
        Targeter = targeter;
    }

    public EAttackType PreferredAttackType { get; set; }

    public EStanceType MovementBehaviour { get; set; }

    public ITarget Targeter { get; private set; }
}
