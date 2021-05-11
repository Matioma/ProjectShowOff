using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class WASDController : MonoBehaviour
{
    protected CharacterController controller;


    public float speed = 6.0f;
    //public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;

    protected Vector3 moveDirection = Vector3.zero;


    


    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }


    private void FixedUpdate()
    {
        if (controller.isGrounded)
        {
            moveDirection = transform.right * Input.GetAxis("Horizontal") + transform.forward * Input.GetAxis("Vertical");
            moveDirection *= speed;

            //if (Input.GetButton("Jump"))
            //{
            //    moveDirection.y = jumpSpeed;
            //}
        }
        SpecialAction();


        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }



    public virtual void SpecialAction() {
    }
}
