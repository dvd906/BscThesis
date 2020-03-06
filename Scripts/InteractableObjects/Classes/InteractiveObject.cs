using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
// TODO: not IInteractionLogic just int as ID
public class InteractiveObject : IUnityInteractiveObject
{
    string uiMessage;
    Dictionary<int, GameObject> storedObjects;

    public event Action<IInteractionLogic> OnRegister;
    public event Action<IInteractionLogic> OnUnRegister;

    public InteractiveObject()
    {
        storedObjects = new Dictionary<int, GameObject>();
    }

    public string UIMessage
    {
        get { return uiMessage; }
        protected set { uiMessage = value; }
    }


    public static IUnityInteractiveObject InteractiveFactory(InteractiveType interactableObject)
    {
        IUnityInteractiveObject obj;
        switch (interactableObject)
        {
            case InteractiveType.Base:
                obj = new InteractiveObject();
                break;
            case InteractiveType.Supply:
                obj = new AmmunitionCrate();
                break;
            case InteractiveType.ObjectiveDescription:
                obj = new Objective();
                break;
            case InteractiveType.Dialog:
                obj = new Dialog();
                break;
            default:
                obj = new InteractiveObject();
                break;
        }
        return obj;
    }

    public virtual bool RegisterObject(GameObject toRegister, IInteractionLogic id)
    {
        if (!storedObjects.ContainsValue(toRegister))
        {
            id.InputChangedWithId += Id_InputChangedWithId;
            storedObjects.Add(id.InteractionID, toRegister);
            if (OnRegister != null)
            {
                OnRegister(id);
            }
            return true;
        }
        return false;
    }

    public virtual bool UnRegisterObject(GameObject toUnRegister, IInteractionLogic id)
    {
        GameObject value;
        if (storedObjects.ContainsValue(toUnRegister) &&
            storedObjects.TryGetValue(id.InteractionID, out value) &&
            value == toUnRegister)
        {
            id.InputChangedWithId -= Id_InputChangedWithId;
            storedObjects.Remove(id.InteractionID);
            if (OnUnRegister != null)
            {
                OnUnRegister(id);
            }
            return true;
        }
        return false;
    }

    protected IUIData GetDesiredUIScript(GameObject gameObject, UIViewType viewType)
    {
        IUIData script = (from act in gameObject.GetComponents<IUIData>()
                          where act.UIType == viewType
                          select act).FirstOrDefault();
        return script;
    }

    protected GameObject GetStoredObject(int key)
    {
        GameObject gameObject;
        storedObjects.TryGetValue(key, out gameObject);
        return gameObject;
    }

    protected virtual void InteractWithGameobject(IInteractebleInput input, int key)
    {
    }

    private void Id_InputChangedWithId(IInteractebleInput input, IComparable id)
    {
        // TODO: for IComparable interface
        int key = (int)id;
        InteractWithGameobject(input, key);
    }



}
