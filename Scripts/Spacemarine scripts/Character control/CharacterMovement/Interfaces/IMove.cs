using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMove
{
    bool IsGrounded { get; }
    void Move(float x, float y, float movementSpeed, float time);
    void Move(Vector3 movementDir, float time);
}
