using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpCameraFollow : ICameraFollow<EMovementID>
{
    bool IsZooming { get; }
    bool IsMeleeViewEnabled { get; set; }
    ISpStateView MeleeView { get; }
    ISpStateView RangedView { get; }
    void SetPosition(EMovementID placeMarker);
    void SetZoom(bool isZooming);
}
