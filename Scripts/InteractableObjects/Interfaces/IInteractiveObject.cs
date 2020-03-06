using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractiveObject<ObjectToRegister, KeyToStore>
    where ObjectToRegister : class
    where KeyToStore : class
{
    string UIMessage { get; }

    event Action<KeyToStore> OnRegister;
    event Action<KeyToStore> OnUnRegister;

    bool RegisterObject(ObjectToRegister toRegister, KeyToStore id);
    bool UnRegisterObject(ObjectToRegister toUnRegister, KeyToStore id);
}
