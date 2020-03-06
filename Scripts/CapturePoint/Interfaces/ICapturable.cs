using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICapturable
{
    event Action<int> Captured;
    event Action<int> Capturing;

    int CaptureID { get; }
    float CaptureInterval { get; }
    float MaxCaptureValue { get; }
    float CurrentCaptureValue { get; }
    float CaptureProportion { get; }

    void IncreaseCapture();
    void DecreaseCaptureNumber();
    void Capture(float time);
}
