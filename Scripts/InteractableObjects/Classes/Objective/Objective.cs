using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Objective : InteractiveObject, IObjective
{

    public string ObjectiveMessage { get; set; }

    public Objective()
    {
        this.ObjectiveMessage = string.Empty;
        this.UIMessage = null;
    }

    public override bool RegisterObject(GameObject toRegister, IInteractionLogic id)
    {
        IUIData script = GetDesiredUIScript(toRegister, UIViewType.Objective);
        if (script != null)
        {
            script.EnableComponent(true);
            script.SetUIMessage(this.ObjectiveMessage);
        }
        return true;
    }

    protected override void InteractWithGameobject(IInteractebleInput input, int key)
    {
        // TODO objective was successful ??? or manager class 
    }

    public override bool UnRegisterObject(GameObject toUnRegister, IInteractionLogic id)
    {
        IUIData script = GetDesiredUIScript(toUnRegister, UIViewType.Objective);
        if (script != null && script.UIType == UIViewType.Objective)
        {
            script.EnableComponent(false);
            script.SetUIMessage(string.Empty);
        }
        return true;
    }
}
