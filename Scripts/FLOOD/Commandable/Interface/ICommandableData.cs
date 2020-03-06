using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICommandableData : IData
{
    ICommandable CommandableUnit { get; set; }
}
