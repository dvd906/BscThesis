using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpVitalityData : IData
{
    ISpLifeData LifeData { get; }
    ISpShieldData ShieldData { get; }
}
