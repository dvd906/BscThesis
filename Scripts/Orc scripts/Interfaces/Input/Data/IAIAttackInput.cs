using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAIAttackInput : IOrcAttackInput
{
    ITarget Targeter { get; }
}
