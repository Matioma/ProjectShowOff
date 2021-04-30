using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class WASDPlayerController : MonoBehaviour
{
    CharacterController controller;

    //public float speed = 12f;
    //public float gravity = -10f;
    //public float jumpHeight = 2f;

    //public Transform groundCheck;
    //public float groundDistance = 0.4f;
    //public LayerMask groundMask;


    //Vector3 velocity;
    //bool isGrounded;



    public float speed = 6.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = -20.0f;

    private Vector3 moveDirection = Vector3.zero;


    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }


    private void FixedUpdate()
    {
        Debug.Log(controller.isGrounded);
        if (controller.isGrounded)
        {

            // We are grounded, so recalculate
            // move direction directly from axes

            moveDirection = transform.right * Input.GetAxis("Horizontal") + transform.forward * Input.GetAxis("Vertical");
            //  new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
            moveDirection *= speed;

            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }
        }

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        moveDirection.y -= gravity * Time.deltaTime;

        // Move the controller
        controller.Move(moveDirection * Time.deltaTime);
    }
}
