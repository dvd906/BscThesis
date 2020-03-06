using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//This code is just for testing not in develop. Public fields will be removed in the real code.
public class SpaceMarineAnimatorTester : MonoBehaviour
{

    //Get which layers are melee and ranged
    public string[] MeleeAnimationsLayers;
    public string[] RangedAttackAnimationLayers;
    //For reset timing in script
    public AnimationClip[] meleeAttacks;
    public AnimationClip[] rangedAttacks;
    //Animator
    public Animator animator;

    float[] meleeAttackTimes;
    float[] rangedAttackTimes;
    


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
