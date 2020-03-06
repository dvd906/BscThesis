using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

public class UIInteractableScript : MonoBehaviour, IUIData
{
    [SerializeField]
    GameObject toEnable;
    [SerializeField]
    Text toModify;

    public bool IsEnabled { get { return enabled; } }

    public UIViewType UIType { get; protected set; }

    private void Start()
    {
        UIType = UIViewType.Basic;
    }

    public virtual void EnableComponent(bool isEnabled)
    {
        if (toEnable != null)
        {
            this.toEnable.SetActive(isEnabled);
        }
    }

    public virtual void SetUIMessage(string message)
    {
        if (toModify != null)
            toModify.text = message;
    }
}
