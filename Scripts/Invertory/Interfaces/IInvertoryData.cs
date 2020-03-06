using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInvertoryData : IData
{
    IInvertory Invertory { get; }
    IInvertoryLogic InvertoryLogic { get; }
}
