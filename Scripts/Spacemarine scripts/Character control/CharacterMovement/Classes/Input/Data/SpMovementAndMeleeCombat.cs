using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpMovementAndMeleeCombat : IMovementAndMeleeCombatInput
{

    float movementPosX;
    float movementPosY;
    bool isAttackingRanged;
    bool isAttackingMelee;
    bool isRunning;
    bool isReloadEnabled;
    int switchWeaponSlot;
    bool isInputChanged;
    bool isFalling;

    Transform rayCastTransform;
    LayerMask collideMask;
    float tresholdToRun;
    float rayCastMax;
    float radius;

    public float MovementPosY
    {
        get { return this.movementPosY; }
        set
        {
            if (!isInputChanged && movementPosY != 0)
            {
                isInputChanged = true;
            }
            movementPosY = value;
        }
    }
    public float MovementPosX
    {
        get { return this.movementPosX; }
        set
        {
            if (!isInputChanged && movementPosX != 0)
            {
                isInputChanged = true;
            }
            movementPosX = value;
        }
    }
    public bool IsAttackingRanged
    {
        get { return this.isAttackingRanged; }
        set
        {
            if (isAttackingRanged != value)
            {
                isAttackingRanged = value;
                if (!isInputChanged)
                {
                    isInputChanged = true;
                }
            }
        }
    }
    public bool IsAttackingMelee
    {
        get { return this.isAttackingMelee; }
        set
        {
            if (isAttackingMelee != value)
            {
                isAttackingMelee = value;
                if (!isInputChanged)
                {
                    isInputChanged = true;
                }
            }
        }
    }
    public bool IsRunning
    {
        get { return this.isRunning; }
        set
        {
            if (isRunning && movementPosY < tresholdToRun)
            {
                isRunning = false;
                if (!isInputChanged)
                {
                    isInputChanged = true;
                }
            }

            if (value == true && !isRunning && movementPosY > tresholdToRun)
            {
                isRunning = true;
                if (!isInputChanged)
                {
                    isInputChanged = true;
                }
            }
        }
    }
    public bool IsReloadEnabled
    {
        get { return this.isReloadEnabled; }
        set
        {
            if (isReloadEnabled != value)
            {
                isReloadEnabled = value;
                if (!isInputChanged)
                {
                    isInputChanged = true;
                }
            }
        }
    }
    public int SwitchToWeaponSlot
    {
        get { return this.switchWeaponSlot; }
        set
        {
            if (switchWeaponSlot != value)
            {
                switchWeaponSlot = value;
                if (!isInputChanged)
                {
                    isInputChanged = true;
                }
            }
        }
    }
    public bool IsFalling
    {
        get
        {
            this.isFalling = !Physics.SphereCast(new Ray(rayCastTransform.position, Vector3.down), radius, rayCastMax, collideMask);
            if (isFalling)
            {
                isRunning = false;
            }
            return this.isFalling;
        }
    }
    public bool IsInputChanged { get { return this.isInputChanged; } }



    public SpMovementAndMeleeCombat(ref Transform modelTransform, LayerMask collideMask, ref CharacterController characterController)
    {
        movementPosX = 0.0f;
        movementPosY = 0.0f;
        isAttackingRanged = false;
        isAttackingMelee = false;
        isRunning = false;
        isReloadEnabled = false;
        switchWeaponSlot = 0;
        tresholdToRun = 0.7f;
        isFalling = false;
        rayCastMax = 4.0f;
        this.rayCastTransform = modelTransform;
        this.collideMask = collideMask;
        this.radius = characterController.radius;
    }

    public void ResetChange()
    {
        this.isInputChanged = false;
    }
}
