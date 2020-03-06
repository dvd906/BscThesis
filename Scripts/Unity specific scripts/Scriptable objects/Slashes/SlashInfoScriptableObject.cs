using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New slashes info", menuName = "Weapons/Information/SlashInfo")]
public class SlashInfoScriptableObject : ScriptableObject, ISlashInfo
{
    #region Private Serialized Fields
    [SerializeField]
    float durationTime;
    [SerializeField]
    float damage;
    #endregion

    public float DurationTime { get { return this.durationTime; } }

    public float Damage { get { return this.damage; } }

    public void MeleeAttack()
    {
    }
}
