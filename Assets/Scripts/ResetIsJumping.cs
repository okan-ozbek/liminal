using System.Collections;
using System.Collections.Generic;
using Player.Enums;
using UnityEngine;

public class ResetIsJumping : StateMachineBehaviour
{
    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool(AnimatorEnum.isJumping.ToString(), false);
    }
}
