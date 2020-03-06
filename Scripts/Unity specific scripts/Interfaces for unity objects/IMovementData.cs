using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The moveable object of the gameplay
public interface IMovementData : IData
{
    ITarget Targeter { get; }
}
