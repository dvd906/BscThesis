using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObjectScript : MonoBehaviour, IInteractiveObjectData
{
    [SerializeField]
    InteractiveType interactableObjectType;
    [HideInInspector]
    [SerializeField]
    DescriptionScriptableObject description;
    [HideInInspector]
    [SerializeField]
    PresentationScriptableObject dialog;

    IUnityInteractiveObject interactiveObject;

    public IUnityInteractiveObject Object
    {
        get { return interactiveObject; }
        set { interactiveObject = value; }
    }

    public bool IsEnabled { get { return this.enabled; } }

    public void EnableComponent(bool isEnabled)
    {
        enabled = isEnabled;
    }

    private void OnEnable()
    {
        this.gameObject.SetActive(true);
        this.enabled = false;
        interactiveObject = InteractiveObject.InteractiveFactory(interactableObjectType);
        if (interactableObjectType == InteractiveType.ObjectiveDescription && description != null)
        {
            (interactiveObject as IObjective).ObjectiveMessage = description.Description;
        }
        else if (interactableObjectType == InteractiveType.Dialog && dialog != null)
        {
            (interactiveObject as IDialog).Title = dialog.Title;
            (interactiveObject as IDialog).BulletPoints = dialog.BulletPoints;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        IInteractableData data = other.GetComponent<IInteractableData>();
        if (data != null)
        {
            interactiveObject.RegisterObject(other.gameObject, data.InteractionLogic);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        IInteractableData data = other.GetComponent<IInteractableData>();
        if (data != null)
        {
            interactiveObject.UnRegisterObject(other.gameObject, data.InteractionLogic);
            if (interactableObjectType != InteractiveType.Supply)
            {
                this.gameObject.SetActive(false);
            }
        }
    }
}
