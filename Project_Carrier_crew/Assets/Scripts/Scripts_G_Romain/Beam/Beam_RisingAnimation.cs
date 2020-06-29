using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam_RisingAnimation : StateMachineBehaviour
{
    public override void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
    {
        animator.SetBool("TryToRaise", false);
        animator.SetBool("PrepareToRaise", false);
        animator.SetBool("RaisingBeam", false);
    }
}
