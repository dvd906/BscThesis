using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeaponInfo : IRangedWeaponInfo
{
    #region Private fields
    int numberOfAmmoInMagazine;
    int maximumMagazineSize;
    int currentAmmoInMagazine;
    int currentAmmoMax;
    float reloadTime;
    float shootDurationTime;
    LayerMask damagableLayer;
    EWeaponType weaponType;
    ISlashLogic meleeProperties;
    Transform bulletSpawn;
    #endregion

    public string WeaponName { get; private set; }

    public int NumberOfAmmoInMagazine { get { return this.numberOfAmmoInMagazine; } }

    public int MaximumAmmunition { get { return maximumMagazineSize * numberOfAmmoInMagazine; } }

    public int ShellDamage { get; private set; }

    public int MaximumMagazineSize { get { return maximumMagazineSize; } }

    public float ReloadTime { get { return this.reloadTime; } }

    public float ShootDurationTime { get { return this.shootDurationTime; } }

    public EWeaponType WeaponType
    {
        get
        {
            if (this.IsMeleeWeapon || this.weaponType == EWeaponType.Melee)
            {
                this.weaponType = EWeaponType.Pistol;
            }
            return this.weaponType;
        }
    }

    public bool IsMeleeWeapon { get { return false; } }

    public ISlashLogic MeleeProperties { get { return this.meleeProperties; } }

    public int CurrentAmmoInMagazine { get { return this.currentAmmoInMagazine; } }

    public int CurrentAmmoMax { get { return this.currentAmmoMax; } }

    public Transform BulletSpawnTransform { get { return this.bulletSpawn; } }

    public RangedWeaponInfo(string weaponName,
        int shellDamage, int numberOfAmmoInMagazine, int maximumMagazineSize,
        float reloadTime, float shootDurationTime, EWeaponType weaponType,
        ISlashLogic meleeProperties, Transform bulletSpawn, LayerMask damagabaleLayer)
    {
        this.WeaponName = weaponName;
        ShellDamage = shellDamage;
        this.numberOfAmmoInMagazine = numberOfAmmoInMagazine;
        this.maximumMagazineSize = maximumMagazineSize;
        this.currentAmmoInMagazine = numberOfAmmoInMagazine;
        this.currentAmmoMax = this.MaximumAmmunition - numberOfAmmoInMagazine;
        this.reloadTime = reloadTime;
        this.shootDurationTime = shootDurationTime;
        this.weaponType = weaponType == EWeaponType.Melee ? EWeaponType.Pistol : weaponType;
        this.meleeProperties = meleeProperties;
        this.bulletSpawn = bulletSpawn;
        this.damagableLayer = damagabaleLayer;
    }

    public void ReFill()
    {
        this.currentAmmoMax = this.MaximumAmmunition - numberOfAmmoInMagazine; // refill takes one magazine
        this.currentAmmoInMagazine = this.numberOfAmmoInMagazine;
    }

    public void Reload()
    {
        if (currentAmmoMax > 0)
        {
            if (currentAmmoMax - numberOfAmmoInMagazine >= numberOfAmmoInMagazine)
            {
                currentAmmoMax = currentAmmoMax - numberOfAmmoInMagazine;
                currentAmmoInMagazine = numberOfAmmoInMagazine;
            }
            else
            {
                currentAmmoInMagazine = currentAmmoMax;
                currentAmmoMax = 0;
            }
        }

    }

    public Vector3 Shoot()
    {
        if (currentAmmoInMagazine > 0)
        {
            currentAmmoInMagazine--;
            RaycastHit hit;
            // TODO instatiate bullet prefab
            if (Physics.Raycast(bulletSpawn.position, bulletSpawn.forward, out hit))
            {
                //Debug.DrawLine(bulletSpawn.position, hit.point, Color.red);
                return hit.point;
            }
        }
        return Vector3.zero;
    }

    public Vector3 Shoot(Transform transform)
    {
        if (currentAmmoInMagazine > 0)
        {
            currentAmmoInMagazine--;
            RaycastHit hit;
            // TODO: distance, bullet prefab enable
            if (Physics.Raycast(transform.position, transform.forward, out hit, 100, damagableLayer))
            {
                IDamagableData info = hit.transform.GetComponentInParent<IDamagableData>();
                if (info != null)
                {
                    info.Character.TakeDamage(ShellDamage); //TODO damage value for weapon
                }
                return hit.point;
            }
        }
        return Vector3.zero;

    }
}
