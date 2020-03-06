using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcAttackLogic : IOrcAttackLogic
{
    IOrcAttackInput input;

    public OrcAttackLogic(IOrcAttackInput input)
    {
        this.input = input;
    }

    public IOrcAttackInput InputInfo { get { return this.input; } }

    public event Action<IOrcAttackInput> InputChanged;

    public bool HasInputModel { get { return input != null; } }

    // No readinput implemented
    public void ReadInput()
    {

    }
    // No reset implemented
    public void Reset()
    {

    }
}
