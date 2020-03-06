using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateVitalityScript : MonoBehaviour
{
    [SerializeField]
    Slider healthSlider;
    [SerializeField]
    Slider shieldSlider;

    ISpLifeData spLife;
    ISpShieldData shield;

    private void Start()
    {
        spLife = GetComponent<ISpLifeData>();
        shield = GetComponent<ISpShieldData>();
    }

    private void Update()
    {
        if (spLife.IsEnabled)
        {
            healthSlider.value = spLife.Life.CurrentLife / spLife.Life.MaximumLife;
            if (spLife.Life.CurrentLife <= 0)
            {
                this.enabled = false;
            }
        }
        if (shield.IsEnabled)
        {
            shieldSlider.value = shield.Shield.CurrentShieldStatus / shield.Shield.MaximumShield;
        }
    }
}
