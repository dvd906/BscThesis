using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRangedWeapon : IWeapon
{
    new IRangedWeaponInfo WeaponInfo { get; }

    Vector3 ShotPosition { get; }
    bool IsShooting { get; }
    bool IsReloadEnabled { get; }
    bool CanShoot { get; }

    void Shoot(Transform camTransform);
    void Reload();
    void ReFill();
}
