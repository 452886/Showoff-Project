﻿using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ScoreUpScript : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Destroy(animator.gameObject, stateInfo.length);
    }
}


