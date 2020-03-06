using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorTester2 : MonoBehaviour
{
    public int MeleeAnimationsCount;
    public int LightDamagedAnimationsCount;

    private Animator animator;

    private float posX, posY;
    private float damageAnimation, attackingAnimation;
    bool isFalling;
    bool isWaitingForRespawn;
    bool isDead;
    bool isLanding;
    bool isDriveUp;
    bool canGetUp;
    bool canMove;
    bool isDamaged;
    bool isAttacking;

    int posXhash, posYhash;
    int damageAnimationHash, attackingAnimationHash;
    int isFallingHash;
    int isWaitingRespawnHash;
    int isDeadHash;
    int isLandingHash;
    int isDriveUpHash;
    int canGetUpHash;
    int canMoveHash;
    int isDamaegedHash;
    int isAttackingHash;

    private float damageStep;
    private float attackStep;
    private float timepassed;

    private enum OrcStates { None, Locomotion, Attacking }

    // Use this for initialization
    void Start()
    {
        this.animator = GetComponentInChildren<Animator>();
        this.posXhash = Animator.StringToHash("PosX");
        this.posYhash = Animator.StringToHash("PosY");
        this.damageAnimationHash = Animator.StringToHash("DamagedAnim");
        this.attackingAnimationHash = Animator.StringToHash("AttackingAnim");
        this.isFallingHash = Animator.StringToHash("IsFalling");
        this.isWaitingRespawnHash = Animator.StringToHash("IsWaitingRespawn");
        this.isDeadHash = Animator.StringToHash("IsDead");
        this.isLandingHash = Animator.StringToHash("IsLanding");
        this.isDriveUpHash = Animator.StringToHash("IsDriveUp");
        this.canGetUpHash = Animator.StringToHash("CanGetUp");
        this.canMoveHash = Animator.StringToHash("CanMove");
        this.isDamaegedHash = Animator.StringToHash("IsDamaged");
        this.isAttackingHash = Animator.StringToHash("IsAttacking");
        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;

        damageStep = LightDamagedAnimationsCount > 2 ? 1 / LightDamagedAnimationsCount : 1;
        attackStep = MeleeAnimationsCount > 2 ? 1 / MeleeAnimationsCount : 1;

        this.canMove = true;
        animator.SetBool(canMoveHash, canMove);
    }

    // Update is called once per frame
    void Update()
    {
        posX = Input.GetAxisRaw("Horizontal");
        posY = Input.GetAxisRaw("Vertical");
        timepassed += Time.deltaTime;
        if (Input.GetButton("Fire1") && !Input.GetButtonDown("Fire1") && !isAttacking)
        {
            isAttacking = true;
            attackingAnimation = attackingAnimation <= 1 ? attackingAnimation + attackStep : 0;
            animator.SetBool(isAttackingHash, isAttacking);
            animator.SetFloat(attackingAnimationHash, attackingAnimation);

           
            float timeToWait = animator.GetNextAnimatorStateInfo(1).length;
            Debug.Log(timepassed);
            timepassed = 0;
            StartCoroutine(ResetValue(timeToWait, OrcStates.Attacking));
        }
    }

    private IEnumerator ResetValue(float timeToWait, OrcStates state)
    {
        yield return new WaitForSeconds(timeToWait);
        switch (state)
        {
            case OrcStates.None:
                break;
            case OrcStates.Locomotion:
                break;
            case OrcStates.Attacking:
                isAttacking = false;
                animator.SetBool(isAttackingHash, isAttacking);
                break;
            default:
                break;
        }
    }
}
