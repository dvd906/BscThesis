using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensitivityCamera : ISensitivity
{
    private float sensitivity;
    private float sensitivityY;

    public float SensitivityX
    {
        get { return this.sensitivity; }
        set { this.sensitivity = value; }
    }

    public float SensitivityY
    {
        get { return this.sensitivityY; }
        set { this.sensitivityY = value; }
    }

    public SensitivityCamera()
    {
    }

    public SensitivityCamera(float sensivityX, float sensivityY)
    {
        this.sensitivity = sensivityX;
        this.sensitivityY = sensivityY;
    }
}
