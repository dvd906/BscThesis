using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvertoryLogic : IInvertoryLogic
{
    IInvertory invertory;
    float scroll;
    int selectedWeaponIdx;

    public IInvertory InputInfo { get { return this.invertory; } }

    public bool IsChangeNeeded { get { return HasInputModel ? this.invertory.IsChangingWeapon : false; } }

    public bool HasInputModel { get { return this.invertory != null; } }

    public event Action<IInvertory> InputChanged;

    public InvertoryLogic() { }

    public InvertoryLogic(ref IInvertory invertory)
    {
        this.invertory = invertory;
    }

    public void ReadInput()
    {
        if (!HasInputModel)
            return;

        if (invertory.IsChangingWeapon)
            return;

        scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0)
        {
            if (scroll > 0)
            {
                InputInfo.SelectedWeaponIndex++;
            }
            else
            {
                InputInfo.SelectedWeaponIndex--;
            }
            ChangeWeapon();
        }
        else
        {

            if (Input.GetButtonDown("WeaponSlot1"))
            {
                selectedWeaponIdx = 0;
            }
            else if (Input.GetButtonDown("WeaponSlot2"))
            {
                selectedWeaponIdx = 1;
            }
            else if (Input.GetButtonDown("WeaponSlot3"))
            {
                selectedWeaponIdx = 2;
            }
            else if (Input.GetButtonDown("WeaponSlot4"))
            {
                selectedWeaponIdx = 3;
            }
            if (selectedWeaponIdx != -1 && selectedWeaponIdx < invertory.NumberOfStashes)
            {
                invertory.SelectedWeaponIndex = selectedWeaponIdx;
                selectedWeaponIdx = -1;
                ChangeWeapon();
            }
        }



    }

    public void Reset()
    {
    }

    private void ChangeWeapon()
    {
        if (InputChanged != null && !invertory.IsChangingWeapon)
        {
            InputChanged(this.invertory);
        }
    }

}
