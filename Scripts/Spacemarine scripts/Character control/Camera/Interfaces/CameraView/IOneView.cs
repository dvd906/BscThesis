using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Includes on view for the camera example: spacemarine melee run cam options
public interface IOneView<Option, Position>
{
    Option CameraOption { get; }
    Position OnePosition { get; }
}
