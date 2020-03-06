using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Commandable Unit", menuName = "Commandable/Movable")]
public class MovableUnitScriptableObject : ScriptableObject
{
    [SerializeField]
    EAttackType startAttackType;
    [SerializeField]
    EStanceType startMovementBehavior;

    public EAttackType StartAttackType { get { return startAttackType; } }

    public EStanceType StartMovementBehavior { get { return startMovementBehavior; } }

}
