using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObjective : IUnityInteractiveObject
{
    string ObjectiveMessage { get; set; }
    // TODO: objective was accomplished for example cpature point captured
}
