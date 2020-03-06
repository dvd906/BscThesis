using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRangedWeaponInfo : IWeaponInfo
{
    //TODO: Interface for bullet prefabs, IRangedWeaponInfo
    Transform BulletSpawnTransform { get; }

    // Start properties
    int ShellDamage { get; }
    int NumberOfAmmoInMagazine { get; }
    int MaximumAmmunition { get; }
    int MaximumMagazineSize { get; }
    float ReloadTime { get; }
    float ShootDurationTime { get; }

    // In game properties
    int CurrentAmmoInMagazine { get; }
    int CurrentAmmoMax { get; }

    void Reload();
    void ReFill();
    Vector3 Shoot();
    Vector3 Shoot(Transform transform);
}
