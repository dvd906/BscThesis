using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRangedCombatLogic : IInputLogic<IRangedCombatInput>
{
    Transform CameraTransform { get; }
}
