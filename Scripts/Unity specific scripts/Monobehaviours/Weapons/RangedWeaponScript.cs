using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeaponScript : MonoBehaviour, IRangedWeaponData
{
    [Header("Transform properties")]
    [SerializeField]
    Transform idle;
    [SerializeField]
    Transform use;

    [Header("Ranged Weapon description")]
    [SerializeField]
    Transform bulletSpawn;
    [SerializeField]
    SlashesListScriptableObject meleeAttacks;
    [SerializeField]
    AnimationScriptableObject weaponAnimations;
    [SerializeField]
    RangedWeaponScriptableObject rangedWeaponInfo;

    float deltaTime = 0;
    IRangedWeapon rangedWeapon;
    IDamagableData damagable;
    LineRenderer weaponRenderer;

    public bool IsEnabled { get; private set; }

    public IRangedWeapon RangedWeapon { get { return this.rangedWeapon; } }
    IWeapon IWeaponData.Weapon { get { return this.rangedWeapon; } }

    public void EnableComponent(bool isEnabled)
    {
        this.enabled = isEnabled;
        this.IsEnabled = isEnabled;
    }

    // Use this for initialization
    void Awake()
    {
        IWeaponAnimation weaponAnimation = new WeaponAnimationInfo(weaponAnimations.OnEquipTime, weaponAnimations.UnequipTime);
        List<ISlashInfo> slashes = new List<ISlashInfo>();
        for (int i = 0; i < meleeAttacks.Slashes.Count; i++)
        {
            slashes.Add(meleeAttacks.Slashes[i]);
        }
        ISlashLogic slashLogic = new SlashLogic(meleeAttacks.TimeToWait, ref slashes);

        IRangedWeaponInfo weaponInfo = new RangedWeaponInfo
            (
                rangedWeaponInfo.WeaponName, rangedWeaponInfo.Damage,
                rangedWeaponInfo.NumberOfAmmoInMagazine, rangedWeaponInfo.MaximumMagazineSize,
                rangedWeaponInfo.ReloadTime, rangedWeaponInfo.ShootDurationTime,
                rangedWeaponInfo.WeaponType, slashLogic,
                bulletSpawn, rangedWeaponInfo.DamagableLayer
            );

        IWeaponPlace weaponPlace = new WeaponPlaces(ref idle, ref use);

        this.rangedWeapon = new RangedWeapon(ref weaponAnimation, ref weaponInfo, ref weaponPlace, this.transform);
        this.rangedWeapon.PlaceWeapon(true);
        this.weaponRenderer = GetComponent<LineRenderer>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!this.rangedWeapon.IsMeleeAttackEnabled)
            return;

        damagable = collision.gameObject.GetComponentInParent<IDamagableData>();
        if (damagable != null)
        {
            damagable.Character.TakeDamage(rangedWeapon.WeaponInfo.MeleeProperties.CurrentSlash.Damage);
        }
    }

    private void Update()
    {

        if (weaponRenderer == null)
            return;

        weaponRenderer.SetPosition(0, this.rangedWeapon.WeaponInfo.BulletSpawnTransform.position);
        weaponRenderer.enabled = this.rangedWeapon.IsShooting;
        weaponRenderer.SetPosition(1, this.rangedWeapon.ShotPosition);

    }
}
