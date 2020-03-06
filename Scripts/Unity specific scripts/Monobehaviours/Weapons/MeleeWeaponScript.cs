using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeaponScript : MonoBehaviour, IWeaponData
{
    [Header("Transform properties")]
    [SerializeField]
    Transform idle;
    [SerializeField]
    Transform use;

    [Header("Melee Weapon description")]
    [SerializeField]
    BasicWeaponInfoScriptableObject baseWeaponInfo;
    [SerializeField]
    SlashesListScriptableObject meleeAttacks;
    [SerializeField]
    AnimationScriptableObject weaponAnimations;

    IWeapon meleeWeapon;
    IDamagableData damagable;

    public IWeapon Weapon { get { return this.meleeWeapon; } }

    public bool IsEnabled { get { return this.enabled; } }

    Collider[] collidersInchildren;

    public void EnableComponent(bool isEnabled)
    {
        this.enabled = isEnabled;
        if (collidersInchildren != null && collidersInchildren.Length > 0)
        {
            for (int i = 0; i < collidersInchildren.Length; i++)
            {
                collidersInchildren[i].enabled = isEnabled;
            }
        }
    }

    private void Awake()
    {
        IWeaponAnimation weaponAnimation = new WeaponAnimationInfo(weaponAnimations.OnEquipTime, weaponAnimations.UnequipTime);
        List<ISlashInfo> slashes = new List<ISlashInfo>();
        for (int i = 0; i < meleeAttacks.Slashes.Count; i++)
        {
            slashes.Add(meleeAttacks.Slashes[i]);
        }
        ISlashLogic slashLogic = new SlashLogic(meleeAttacks.TimeToWait, ref slashes);

        IWeaponInfo weaponInfo = new MeleeWeaponInfo(slashLogic, baseWeaponInfo.WeaponName);

        IWeaponPlace weaponPlace = new WeaponPlaces(ref idle, ref use);

        this.meleeWeapon = new Weapon(ref weaponAnimation, ref weaponInfo, ref weaponPlace, this.transform);
        this.meleeWeapon.PlaceWeapon(true);
    }

    private void OnEnable()
    {
        collidersInchildren = GetComponentsInChildren<Collider>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!meleeWeapon.IsMeleeAttackEnabled)
        {
            return;
        }
        damagable = collision.gameObject.GetComponentInParent<IDamagableData>();
        if (damagable != null)
        {
            damagable.Character.TakeDamage(meleeWeapon.WeaponInfo.MeleeProperties.CurrentSlash.Damage);
        }
    }
}
