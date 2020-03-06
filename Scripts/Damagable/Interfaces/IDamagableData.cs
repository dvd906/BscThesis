using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagableData : IData
{
    IDamagable Character { get; }
}
