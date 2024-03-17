using Cinemachine;
using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class RailController : MonoBehaviour
{
    public Transform[] wayPoint;
    public bool isAccelerate;
    public bool isSeatedInCart;
    public bool isStopping;
    int currentWayPoint;

    public float speed;
    public float maxSpeed;

    
   // private Rigidbody rb;

    public CinemachineFreeLook RailCam;
    public Transform player;
    public ParticleSystem[] frictionEfx;
    bool playOnce=false;
    bool playTrainRunSoundOnce = false;
    private void Start()
    {
        //rb = GetComponent<Rigidbody>();
        RailCam.Priority = 0;
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
            player.GetComponentInChildren<CharacterController>().enabled =false;
            player.GetComponentInChildren<ThirdPersonController>().enabled =false;
            RailCam.Priority = 20;
            
            
        }

        if (Input.GetKey(KeyCode.G))
        {
            isStopping = true;
           
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
                if (speed <= maxSpeed && isStopping==false)
                {
                    speed += 2f * Time.deltaTime;
                    AudioManager.instance.trainRun.volume += .1f*Time.deltaTime;
                    if (!playTrainRunSoundOnce)
                    {
                        AudioManager.instance.trainRun.Play();
                       
                        playTrainRunSoundOnce = true;
                    }
                    
                }
                else if (isStopping == true)
                {
                    speed -= 4f * Time.deltaTime;
                    AudioManager.instance.trainRun.Stop();
                    if (!playOnce && speed>=10)
                    {
                        AudioManager.instance.audioSource.PlayOneShot(AudioManager.instance.trainStop);
                        playOnce =true;
                    }
                   
                    for (int i = 0; i < frictionEfx.Length; i++)
                    {
                        
                        frictionEfx[i].Play();
                    }
                    if (speed <= 0)
                    {
                        playOnce = false;
                        playTrainRunSoundOnce = false;
                        AudioManager.instance.trainRun.volume = 0;
                        speed = 0;
                        isAccelerate=false;
                        isStopping=false;
                        RailCam.Priority = 0;
                        player.GetComponentInChildren<CharacterController>().enabled = true;
                        player.GetComponentInChildren<ThirdPersonController>().enabled = true;
                        for (int i = 0; i < frictionEfx.Length; i++)
                        {
                            frictionEfx[i].Stop();
                        }
                    }
                        
                }

                transform.position = Vector3.MoveTowards(transform.position, wayPoint[currentWayPoint].position, speed * Time.deltaTime);
                Vector3 directionToPatrolPoint = (wayPoint[currentWayPoint].position - transform.position).normalized;

                // Rotate to face the patrol point
                Quaternion targetRotation = Quaternion.LookRotation(directionToPatrolPoint);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 5f * Time.deltaTime);

                
                    

            }


        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           
            isSeatedInCart = true;
            UiManager.instance.UpdateInfoText("PRESS ' R ' TO START THE RAIL CART.                      " +
                "PRESS ' G ' TO STOP THE RAIL CART.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            isSeatedInCart = false;
            UiManager.instance.UpdateInfoText(string.Empty);
        }
    }

}
