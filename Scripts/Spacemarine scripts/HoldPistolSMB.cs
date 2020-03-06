using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldPistolSMB : StateMachineBehaviour
{
    [SerializeField]
    private HoldPistolInformation holdPistolInformation;

    IHoldPistolAnimation holdPistolAnimation;
    //public float waitTimeInSeconds = 5.0f;
    //public string LowerWeaponString;

    //private float currentPassed;
    //private int lowerWeaponHash;
    //private static int notSetHash = -1;

    private void Awake()
    {
        //if (LowerWeaponString != string.Empty)
        //{
        //    lowerWeaponHash = Animator.StringToHash(LowerWeaponString);
        //}
        holdPistolAnimation = new HoldPistolOld();
        holdPistolAnimation.SetupObject(holdPistolInformation);

    }

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //currentPassed = 0.0f;
        holdPistolAnimation.Enter(animator);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //currentPassed += Time.deltaTime;
        //if (currentPassed > waitTimeInSeconds && lowerWeaponHash != notSetHash)
        //{
        //    animator.SetBool(lowerWeaponHash, true);
        //}
        holdPistolAnimation.Update(animator);
    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //if (currentPassed > waitTimeInSeconds && lowerWeaponHash != notSetHash)
        //{
        //    animator.SetBool(lowerWeaponHash, false);
        //}
        holdPistolAnimation.Exit(animator);
    }

    // OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}
}
