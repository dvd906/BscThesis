using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDialogData : IUIData
{
    void SetTitle(string title);
    void SetFormattedString(string formatted);
}
