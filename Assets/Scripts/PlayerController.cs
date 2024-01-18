using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Controller")]
    public Animator animator;
    [Range(0, 100)]
    [SerializeField] float movementSpeed;
    [SerializeField] float smoothTime;
    [SerializeField] CharacterController characterController;
    [SerializeField] Transform cam;
   // [SerializeField] ParticleSystem dustPartical;
    Vector3 moveInput;

    public bool canMove;


    private void Start()
    {
        canMove = true;
    }


    private void FixedUpdate()
    {
        CharacterLocomotion();
    }

    void CharacterLocomotion()
    {
       if(canMove)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            Vector3 direction = Quaternion.Euler(0, cam.transform.eulerAngles.y, 0) * new Vector3(horizontal, 0, vertical);
            float InputMag = Mathf.Clamp01(direction.magnitude);
            moveInput = direction.normalized;

            if (InputMag > 0.0f)
            {
                animator.SetFloat("Locomotion", direction.magnitude);
               // dustPartical.Play();
            }
            else
            {
                animator.SetFloat("Locomotion", 0);
              //  dustPartical.Stop();
            }

            if (moveInput != Vector3.zero)
            {
                Quaternion desiredRotation = Quaternion.LookRotation(moveInput, Vector3.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, smoothTime * Time.deltaTime);
            }

            characterController.Move(moveInput * movementSpeed * Time.fixedDeltaTime);
            
        }
    }
}
