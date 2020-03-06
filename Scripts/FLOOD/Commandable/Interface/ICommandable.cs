using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICommandable
{
    EAttackType PreferredAttackType { get; set; }
    EStanceType MovementBehaviour { get; set; }
    ITarget Targeter { get; }
}
