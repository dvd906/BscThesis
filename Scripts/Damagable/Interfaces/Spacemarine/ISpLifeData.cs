using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpLifeData : IData
{
    ISpLifeInfo Life { get; }
}
