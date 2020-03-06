using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmunitionCrate : InteractiveObject
{
    public AmmunitionCrate()
    {
        this.UIMessage = "Resupply Ammunition";
    }

    public override bool RegisterObject(GameObject toRegister, IInteractionLogic id)
    {
        bool wasSuccessFullRegister = base.RegisterObject(toRegister, id);
        if (wasSuccessFullRegister)
        {
            IUIData script = GetDesiredUIScript(toRegister, UIViewType.Supply);
            if (script != null)
            {
                script.EnableComponent(true);
                script.SetUIMessage(this.UIMessage);
            }
        }
        return wasSuccessFullRegister;
    }

    public override bool UnRegisterObject(GameObject toUnRegister, IInteractionLogic id)
    {
        IUIData script = GetDesiredUIScript(toUnRegister, UIViewType.Supply);
        if (script != null && script.UIType == UIViewType.Supply)
        {
            script.EnableComponent(false);
            script.SetUIMessage(string.Empty);
        }
        return base.UnRegisterObject(toUnRegister, id);
    }

    protected override void InteractWithGameobject(IInteractebleInput input, int key)
    {
        GameObject gameObject = GetStoredObject(key);
        IInvertoryData invertoryData = gameObject.GetComponent<IInvertoryData>();
        if (invertoryData != null && invertoryData.Invertory != null)
        {
            invertoryData.Invertory.ResupplyWeapons();
        }
    }
}
