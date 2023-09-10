using System.Collections;
using System.Collections.Generic;
using Player.Enums;
using UnityEngine;

public class ResetBool : StateMachineBehaviour
{
    public bool isInteractingStatus;
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       animator.SetBool(AnimatorEnum.isInteracting.ToString(), isInteractingStatus);
    }
}
