using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUIData : IData
{
    void SetUIMessage(string message);
    UIViewType UIType { get; }
}
