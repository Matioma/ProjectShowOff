using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class WASDPlayerController : MonoBehaviour
{
    CharacterController controller;

    public float speed = 12f;
    public float gravity = -10f;
    public float jumpHeight = 2f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;


    Vector3 velocity;
    bool isGrounded;


    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        float x;
        float z;
        bool jumpPressed = false;

        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");
        jumpPressed = Input.GetButtonDown("Jump");

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded) {
            //velocity.y = 0;
        }

        //Move Horizontally
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);
        if (jumpPressed && isGrounded)
        {
            velocity.y = jumpHeight;
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
