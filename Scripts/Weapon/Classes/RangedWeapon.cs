using UnityEngine;

public class RangedWeapon : IRangedWeapon
{
    bool isMeleeAttackEnabled;
    bool canShoot;
    bool canUse;
    bool reloadEnabled;
    Transform weaponTransform;
    IWeaponAnimation weaponAnimation;
    IWeaponPlace weaponPlace;

    IRangedWeaponInfo rangedWeapon;
    int lastAmmoCount;

    public RangedWeapon(ref IWeaponAnimation weaponAnimation, ref IRangedWeaponInfo rangedWeapon,
        ref IWeaponPlace weaponPlace, Transform weaponTransform)
    {
        this.lastAmmoCount = rangedWeapon.CurrentAmmoInMagazine;
        this.weaponTransform = weaponTransform;
        this.weaponAnimation = weaponAnimation;
        this.weaponPlace = weaponPlace;
        this.rangedWeapon = rangedWeapon;
        this.canShoot = true;
        this.canUse = false;
        this.reloadEnabled = false;
    }

    public IRangedWeaponInfo WeaponInfo { get { return this.rangedWeapon; } }

    public bool IsMeleeAttackEnabled
    {
        get { return this.isMeleeAttackEnabled; }
        set { isMeleeAttackEnabled = value; }
    }

    public bool CanUse { get { return this.canUse; } }

    public Transform WeaponTransform { get { return this.weaponTransform; } }

    public IWeaponAnimation EquipAnimationTime { get { return this.weaponAnimation; } }

    public IWeaponPlace WeaponPlace { get { return this.weaponPlace; } }

    public bool CanShoot { get { return this.canShoot; } }

    public bool IsReloadEnabled { get { return reloadEnabled; } }

    public Vector3 ShotPosition { get; private set; }

    public bool IsShooting
    {
        get
        {
            if (IsReloadEnabled && 
                WeaponInfo.CurrentAmmoInMagazine != lastAmmoCount)
            {
                lastAmmoCount = this.WeaponInfo.CurrentAmmoInMagazine;
                return true;
            }
            return false;
        }
    }

    IWeaponInfo IWeapon.WeaponInfo { get { return this.rangedWeapon; } }

    public int Damage()
    {
        return rangedWeapon.MeleeProperties.CurrentSlashIndex;
    }

    public void PlaceWeapon(bool isPlaceToIdle)
    {
        if (isPlaceToIdle)
        {
            weaponTransform.parent = weaponPlace.Idle;
            weaponTransform.position = weaponPlace.Idle.position;
            weaponTransform.rotation = weaponPlace.Idle.rotation;
            canUse = false;
        }
        else
        {
            weaponTransform.parent = weaponPlace.Use;
            weaponTransform.position = weaponPlace.Use.position;
            weaponTransform.rotation = weaponPlace.Use.rotation;
            canUse = true;
        }
    }

    public void PlaceWeapon(bool isPlaceToIdle, float elapsedTime)
    {
        if (isPlaceToIdle && elapsedTime > weaponAnimation.UnequipTime)
        {
            weaponTransform.parent = weaponPlace.Idle;
            weaponTransform.position = weaponPlace.Idle.position;
            weaponTransform.rotation = weaponPlace.Idle.rotation;
            canUse = false;
        }
        else if (elapsedTime > weaponAnimation.OnEquipTime)
        {
            weaponTransform.parent = weaponPlace.Use;
            weaponTransform.position = weaponPlace.Use.position;
            weaponTransform.rotation = weaponPlace.Use.rotation;
            canUse = true;
        }
    }

    public void ReFill()
    {
        rangedWeapon.ReFill();
        this.canShoot = true;
    }

    public void Reload()
    {
        reloadEnabled = false;
        rangedWeapon.Reload();
        this.canShoot = true;
    }

    public void Shoot(Transform camTransform)
    {
        if (!canShoot || isMeleeAttackEnabled)
        {
            return;
        }
        this.ShotPosition = rangedWeapon.Shoot(camTransform);
        if (!reloadEnabled)
        {
            reloadEnabled = true;
        }
        else if (this.rangedWeapon.CurrentAmmoInMagazine == 0)
        {
            this.canShoot = false;
        }
    }
}
