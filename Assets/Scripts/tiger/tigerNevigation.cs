using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class tigerNevigation : MonoBehaviour
{

    private NavMeshAgent m_Agent;
    public Transform[] wayPoints;
    [SerializeField] int curruntPos;


    private Animator animator;


    private void Start()
    {
        m_Agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        Transform wp = wayPoints[curruntPos];
    }

    private void Update()
    {
        /* float distanceBetweenPoints = Vector3.Distance(transform.position, wayPoints[curruntPos].position);

         if (distanceBetweenPoints <= 4f)
         {
             curruntPos = (curruntPos + 1) % wayPoints.Length;

         }
         else
         {
             m_Agent.destination = wayPoints[curruntPos].position;

         }
        */

        ChasePlayer();
       
    }

    public void ChasePlayer()
    {
        m_Agent.destination = GameObject.FindGameObjectWithTag("Player").transform.position;
        animator.Play("Run");
    }


}
