using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class attackState : StateMachineBehaviour
{


    NavMeshAgent agent;
    Transform playerPosition;
    float attackingDistance = 10f;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        playerPosition = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        agent = animator.GetComponent<NavMeshAgent>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.transform.LookAt(playerPosition);

        float distanceBetweenPlayerandEnemy = Vector3.Distance(playerPosition.position, animator.GetComponent<Transform>().position);

       // Debug.Log(distanceBetweenPlayerandEnemy);
        if (distanceBetweenPlayerandEnemy >= attackingDistance)
        {
            animator.SetBool("isAttacking", false);
        }
       
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void //OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
   // {
      // Implement code that processes and affects root motion
   // }

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
