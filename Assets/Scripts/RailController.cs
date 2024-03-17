using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class RailController : MonoBehaviour
{
    public Transform[] wayPoint;
    public bool isAccelerate;
    public bool isSeatedInCart;
    int currentWayPoint;
    public float speed;
    private Rigidbody rb;


    public Transform player;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
       
    }

    private void FixedUpdate()
    {
        Transform w = wayPoint[currentWayPoint];

        if (Input.GetKey(KeyCode.R) && isSeatedInCart==true)
        {
            isAccelerate = true;
            player.transform.parent=this.transform;
        }

        if (Input.GetKey(KeyCode.G))
        {
            isAccelerate = false;
        }


        if (isAccelerate)
        {


            float distanceBetweenPoints = Vector3.Distance(transform.position, wayPoint[currentWayPoint].position);

            if (distanceBetweenPoints <= 1f)
            {
                currentWayPoint = (currentWayPoint + 1) % wayPoint.Length;
            }
            else
            {
                if (speed <= 10)
                    speed += .5f * Time.deltaTime;
                    rb.position = Vector3.MoveTowards(transform.position, wayPoint[currentWayPoint].position, speed * Time.deltaTime);
                Vector3 directionToPatrolPoint = (wayPoint[currentWayPoint].position - transform.position).normalized;

                // Rotate to face the patrol point
                Quaternion targetRotation = Quaternion.LookRotation(directionToPatrolPoint);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation,5 * Time.deltaTime);

            }


        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           
            isSeatedInCart = true;
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            isSeatedInCart = false;

        }
    }

}
