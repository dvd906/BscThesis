using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//This class is for testing. Includes logic and animation which is not correct.
public class AnimatorTesterOrcControllerNew : MonoBehaviour
{
    //For the editor
    public int StartTestLife = 3;
    public int MeleeAttackAnimationsNumber;
    public int DamagedAnimationsNumber;
    public bool IsRanged;
    public float LastMeleeAnimationTreshold;
    //We presumes that we have one shooting animation
    public float ShootAnimationTreshold;
    public AnimationClip ShootClipInfo;
    public AnimationClip[] GetUpAnimations;
    public float SmoothingFactorGetUpAnimation;

    public AnimationClip[] MeleeAnimations;
    public float SmoothingMeleeAnimations;

    //The animator of the current object
    Animator characterAnimator;
    //Blend tree 2D and 1D
    float posX;
    float posY;
    float attackingAnim = 0;
    float damagedAnim = 0;
    //Chnage the state during animation
    bool canMove;
    bool isFalling;
    bool isDead;
    bool isDriveUp;
    bool isDamaged;
    bool isAttacking;
    //Hashes of the animator
    int posXhash, posYhash;
    int damageAnimationHash, attackingAnimationHash;
    int isFallingHash;
    int isDeadHash;
    int isDriveUpHash;
    int canMoveHash;
    int isDamagedHash;
    int isAttackingHash;
    int isRangedAttackHash;
    int meleeAttackIndexHash;
    int rangedAttacIndexHash;
    //For 1D blend tree animations step to change
    float damageStep;
    float attackStep;
    float life;
    float shootAnimationTreshold;
    float lastMeleeTreshold;
    float shootTime;
    float getUpAnimationTime;
    float getUpSmoothFactor;

    bool meleeToRangedSwitch;
    bool isRanged;

    float[] meleeAnimationTimes;
    //Use one state for one attack
    int currentMeleeAnimationIndex;
    int maxMeleeAnimations;
    int currentRangedAnimationIndex;
    int maxRangedAnimations;
    float smoothingMelee = 1.0f;

    private enum States { Attacking, Damaged, DriveUp }

    // Use this for initialization
    void Start()
    {
        //Using hashes to set the animations
        characterAnimator = GetComponentInChildren<Animator>();
        posXhash = Animator.StringToHash("PosX");
        posYhash = Animator.StringToHash("PosY");
        damageAnimationHash = Animator.StringToHash("DamagedAnim");
        attackingAnimationHash = Animator.StringToHash("AttackingAnim");
        isFallingHash = Animator.StringToHash("IsFalling");
        isDeadHash = Animator.StringToHash("IsDead");
        isDriveUpHash = Animator.StringToHash("IsDriveUp");
        canMoveHash = Animator.StringToHash("CanMove");
        isDamagedHash = Animator.StringToHash("IsDamaged");
        isAttackingHash = Animator.StringToHash("IsAttacking");
        isRangedAttackHash = Animator.StringToHash("IsRangedAttack");
        rangedAttacIndexHash = Animator.StringToHash("RangedAttackIndex");
        meleeAttackIndexHash = Animator.StringToHash("MeleeAttackIndex");

        //Basic animation setup
        canMove = true;
        characterAnimator.SetBool(canMoveHash, true);
        lastMeleeTreshold = LastMeleeAnimationTreshold;
        //Setup the attacks
        maxRangedAnimations = 1;
        currentRangedAnimationIndex = 0;
        maxMeleeAnimations = MeleeAttackAnimationsNumber;
        currentMeleeAnimationIndex = 0;

        shootAnimationTreshold = ShootAnimationTreshold;
        isRanged = IsRanged;

        //Shooting animation setup
        if (ShootClipInfo != null)
            shootTime = ShootClipInfo.length;
        //Get the time when not to use weapon
        if (GetUpAnimations != null)
        {
            int length = GetUpAnimations.Length;
            for (int i = 0; i < length; i++)
            {
                getUpAnimationTime += GetUpAnimations[i].length;
            }
        }
        else
        {
            getUpAnimationTime = 1.5f;
        }
        getUpSmoothFactor = SmoothingFactorGetUpAnimation > 0 ? SmoothingFactorGetUpAnimation : 1;//smooth factor
        getUpAnimationTime *= getUpSmoothFactor;
        life = StartTestLife;

        //Set up the melee animations
        smoothingMelee = SmoothingMeleeAnimations > 0 ? SmoothingMeleeAnimations : 1;

        if (MeleeAnimations != null && MeleeAnimations.Length > 0)
        {
            int length = MeleeAnimations.Length;

            meleeAnimationTimes = new float[length];
            for (int i = 0; i < length; i++)
            {
                meleeAnimationTimes[i] = MeleeAnimations[i].length * smoothingMelee;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        UpdateAnimation(characterAnimator);
    }

    private void UpdateAnimation(Animator characterAnimator)
    {
        if (!isDead)
        {
            float posX = Input.GetAxisRaw("Horizontal");
            float posY = Input.GetAxisRaw("Vertical");
            characterAnimator.SetFloat(posXhash, posX);
            characterAnimator.SetFloat(posYhash, posY);
            //Damaged, left click is true
            if (Input.GetMouseButton(0) && canMove && !isDamaged)
            {
                TakeDamage();
                if (!isDead)
                {
                    isDamaged = true;
                    characterAnimator.SetBool(isDamagedHash, true);


                    damagedAnim = (damagedAnim += damageStep) > 1 ? 0 : damagedAnim;
                    characterAnimator.SetFloat(damageAnimationHash, damagedAnim);

                    //Reset after the time
                    StartCoroutine(ResetValues(States.Damaged, 1.0f));
                }

            }
            //Attack and not damaged, right click is true
            if (Input.GetMouseButton(1) && canMove && !isAttacking)
            {
                isAttacking = true;
                characterAnimator.SetBool(isAttackingHash, true);
                float attackTime;
                if (meleeToRangedSwitch && isRanged)
                {
                    currentRangedAnimationIndex = currentRangedAnimationIndex < maxRangedAnimations ? currentRangedAnimationIndex : 0;
                    characterAnimator.SetInteger(rangedAttacIndexHash, currentRangedAnimationIndex);
                    currentRangedAnimationIndex++;

                    attackTime = shootTime;
                }
                else
                {
                    currentMeleeAnimationIndex = currentMeleeAnimationIndex < maxMeleeAnimations ? currentMeleeAnimationIndex : 0;
                    characterAnimator.SetInteger(meleeAttackIndexHash, currentMeleeAnimationIndex);
                    currentMeleeAnimationIndex++;

                    if (meleeAnimationTimes != null)
                    {
                        currentMeleeAnimationIndex = currentMeleeAnimationIndex < meleeAnimationTimes.Length ? currentMeleeAnimationIndex : 0;
                        attackTime = meleeAnimationTimes[currentMeleeAnimationIndex];
                    }
                    else
                    {
                        attackTime = 1.625f;
                    }
                }


                //Reset after time
                StartCoroutine(ResetValues(States.Attacking, attackTime));
            }
            //Is drive up by somebody, triggered action, space key
            if (Input.GetKey(KeyCode.Space) && !isDriveUp)
            {
                isDriveUp = true;
                canMove = false;
                characterAnimator.SetBool(isDriveUpHash, true);
                characterAnimator.SetBool(canMoveHash, false);
                TakeDamage();
                //Reset after time
                StartCoroutine(ResetValues(States.DriveUp, getUpAnimationTime));
            }
            //Is falling, F key, just falling idle
            if (Input.GetKey(KeyCode.F) && canMove)
            {
                isFalling = true;
                canMove = false;
                characterAnimator.SetBool(isFallingHash, true);
                characterAnimator.SetBool(canMoveHash, false);
            }
            //Landed on the ground
            if (Input.GetKey(KeyCode.G) && isFalling)
            {
                isFalling = false;
                canMove = true;
                characterAnimator.SetBool(isFallingHash, false);
                characterAnimator.SetBool(canMoveHash, true);
            }
            if (Input.GetAxis("Mouse ScrollWheel") != 0 && isRanged)
            {
                //If false than we change the animation
                meleeToRangedSwitch = meleeToRangedSwitch ? false : true;
                characterAnimator.SetBool(isRangedAttackHash, meleeToRangedSwitch);
            }

        }

        //Respawn button
        if (Input.GetKey(KeyCode.R) && isDead)
        {
            isDead = false;
            canMove = true;
            characterAnimator.SetBool(isDeadHash, false);
            life = StartTestLife;
            characterAnimator.SetBool(canMoveHash, true);
            //characterAnimator.SetLayerWeight(1, 1);
            //characterAnimator.SetLayerWeight(2, 1);
        }
    }

    private void TakeDamage()
    {
        life -= 1;
        isDead = life <= 0 ? true : false;
        if (isDead)
        {
            canMove = false;
            characterAnimator.SetBool(isDeadHash, true);
            characterAnimator.SetBool(canMoveHash, false);
            //characterAnimator.SetBool(isDriveUpHash, false);
            //characterAnimator.SetLayerWeight(1, 0);
            //characterAnimator.SetLayerWeight(2, 0);
        }

    }

    IEnumerator ResetValues(States currentAnimatedState, float timeTowait)
    {
        yield return new WaitForSeconds(timeTowait);
        switch (currentAnimatedState)
        {
            case States.Attacking:
                characterAnimator.SetBool(isAttackingHash, false);
                isAttacking = false;
                break;
            case States.Damaged:
                characterAnimator.SetBool(isDamagedHash, false);
                isDamaged = false;
                break;
            case States.DriveUp:
                characterAnimator.SetBool(isDriveUpHash, false);
                if (!isDead)
                    characterAnimator.SetBool(canMoveHash, true);
                isDriveUp = false;
                canMove = true;
                break;
            default:
                break;
        }
    }
}
