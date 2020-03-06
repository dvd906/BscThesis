using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Dialog : InteractiveObject, IDialog
{
    const string NEW_LINE = "\n";
    StringBuilder stringBuilder;

    public Dialog()
    {
        stringBuilder = new StringBuilder();
        this.UIMessage = "Dialog UI Message";
    }

    public string Title { get; set; }

    public string[] BulletPoints { get; set; }

    public string GetFormattedBulletPoints()
    {
        if (BulletPoints != null && BulletPoints.Length != 0)
        {
            foreach (string item in BulletPoints)
            {
                stringBuilder.Append(item);
                stringBuilder.Append(NEW_LINE);
            }
        }
        return stringBuilder.ToString();
    }

    public override bool RegisterObject(GameObject toRegister, IInteractionLogic id)
    {
        bool wasSuccessFullRegister = base.RegisterObject(toRegister, id);
        if (wasSuccessFullRegister)
        {
            IUIData script = GetDesiredUIScript(toRegister, UIViewType.Dialog);
            if (script != null && script is IDialogData)
            {
                IDialogData data = (IDialogData)script;
                data.EnableComponent(true);
                data.SetTitle(this.Title);
                data.SetFormattedString(this.GetFormattedBulletPoints());
            }
        }
        return wasSuccessFullRegister;
    }

    public override bool UnRegisterObject(GameObject toUnRegister, IInteractionLogic id)
    {
        IUIData script = GetDesiredUIScript(toUnRegister, UIViewType.Dialog);
        if (script != null && script.UIType == UIViewType.Dialog)
        {
            IDialogData data = (IDialogData)script;
            data.EnableComponent(false);
            data.SetTitle(string.Empty);
            data.SetFormattedString(string.Empty);
        }
        return base.UnRegisterObject(toUnRegister, id);
    }
}
