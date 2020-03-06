using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovementInput : IInput
{
    float MovementPosY { get; set; }
    float MovementPosX { get; set; }
    bool IsRunning { get; set; }
    bool IsFalling { get; }
}
