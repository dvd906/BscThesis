using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class AnimatorTester : MonoBehaviour
{

    public Animator animator;
    public string animationName;
    public int MeleeAnimationsNumber;

    float meleeAttack;
    float step;
    bool isMeleeAttackPlaying;
    bool isDamagePlaying;
    bool isDead;
    bool isDrivenUp;
    int damagedHash;
    int attackhash;
    int meleeAttackFloatHash;
    int driveUpHash;
    int isFallingHash;

    enum MovementState { None, Attack, Damaged, DriveUp }

    // Use this for initialization
    void Start()
    {
        isMeleeAttackPlaying = false;
        if (MeleeAnimationsNumber > 2)
        {
            step = 1 / MeleeAnimationsNumber;
        }
        else
        {
            step = 1;
        }
        damagedHash = Animator.StringToHash("IsDamaged");
        attackhash = Animator.StringToHash("IsAttacking");
        meleeAttackFloatHash = Animator.StringToHash("MeleeAttack");
        driveUpHash = Animator.StringToHash("IsDroveUp");
        isFallingHash = Animator.StringToHash("IsFalling");

    }

    // Update is called once per frame
    void Update()
    {

        if (!isDead)
        {
            Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxisRaw("Vertical"));
            animator.SetFloat("PosX", input.x);
            animator.SetFloat("PosY", input.y);
            if (Input.GetButton("Fire1") && !isMeleeAttackPlaying && !isDamagePlaying && !isDrivenUp)
            {
                meleeAttack = meleeAttack == 1 ? 0 : meleeAttack + step;
                isMeleeAttackPlaying = true;
                animator.SetBool(attackhash, true);
                animator.SetFloat(meleeAttackFloatHash, meleeAttack);
                float time = animator.GetCurrentAnimatorStateInfo(1).length;
                StartCoroutine(StopAttack(time, MovementState.Attack));
            }
        }

        if (Input.GetKey(KeyCode.Space))
        {
            animator.SetBool("IsDead", true);
            isDead = true;
        }

        if (Input.GetButton("Fire2") && !isDamagePlaying)
        {
            isDamagePlaying = true;
            animator.SetBool(damagedHash, true);
            float time = animator.GetCurrentAnimatorStateInfo(1).length;
            StartCoroutine(StopAttack(time, MovementState.Damaged));

        }

        if (Input.GetKey(KeyCode.E) && !isDrivenUp)
        {
            isDrivenUp = true;
            animator.SetBool(driveUpHash, true);
            AnimatorStateInfo next = animator.GetNextAnimatorStateInfo(0);
            float time = animator.GetCurrentAnimatorStateInfo(0).length + next.length;
            StartCoroutine(StopAttack(time, MovementState.DriveUp));
        }

    }

    private IEnumerator StopAttack(float time, MovementState movementState)
    {
        yield return new WaitForSeconds(time);
        Debug.Log("Couroutine started with: time " + time + " s and the state " + movementState);
        switch (movementState)
        {
            case MovementState.Attack:
                isMeleeAttackPlaying = false;
                animator.SetBool(attackhash, false);
                break;
            case MovementState.Damaged:
                isDamagePlaying = false;
                animator.SetBool(damagedHash, false);
                break;
            case MovementState.DriveUp:
                isDrivenUp = false;
                animator.SetBool(driveUpHash, false);
                break;
        }

    }
}
