using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpStateView : ISpStateView
{
    ISpView basicView;
    ISpView aimView;
    ISpView runView;

    public ISpView BasicView { get { return this.basicView; } }

    public ISpView AimView { get { return this.aimView; } }

    public ISpView RunView { get { return this.runView; } }

    public SpStateView(ISpView basicView, ISpView aimView, ISpView runView)
    {
        this.basicView = basicView;
        this.aimView = aimView;
        this.runView = runView;
    }
}
