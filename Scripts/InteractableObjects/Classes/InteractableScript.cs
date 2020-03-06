using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// One character can interact with other objects, this script holds the object for it
public class InteractableScript : MonoBehaviour, IInteractableData
{

    IInteractebleInput interactebleInput;
    IInteractionLogic interactionLogic;

    public IInteractionLogic InteractionLogic { get { return interactionLogic; } }

    public bool IsEnabled { get { return this.enabled; } }

    public void EnableComponent(bool isEnabled)
    {
        this.enabled = isEnabled;
    }

    // Use this for initialization
    void Start()
    {
        interactebleInput = new InteractableInput();
        interactionLogic = new InteractionLogic(ref interactebleInput, this.gameObject.GetInstanceID());
    }

    // Update is called once per frame
    void Update()
    {
        InteractionLogic.ReadInput();
    }
}
