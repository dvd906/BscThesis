using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICaptureData : IData
{
    ICapturable Capturable { get; }
}
