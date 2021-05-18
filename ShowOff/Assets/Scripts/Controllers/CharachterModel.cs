using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharachterModel : MonoBehaviour, ICharacterController
{
    protected CharacterController controller;


    public float speed = 6.0f;
    public float gravity = 20.0f;

    protected Vector3 moveDirection = Vector3.zero;

    public void AddAceeleration(Vector3 velocity) {
        moveDirection.y = 0;
        moveDirection += velocity;
    }



    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }


    private void FixedUpdate()
    {
        //if (controller.isGrounded)
        //{
        //    moveDirection = transform.right * Input.GetAxis("Horizontal") + transform.forward * Input.GetAxis("Vertical");
        //    moveDirection *= speed;

        //}
        //SpecialAction();


        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }



    public virtual void SpecialAction() {}

    public void Move(Vector3 direction)
    {
        //if (controller.isGrounded)
        //{

        Vector3 xyDirection = direction * speed;
            moveDirection.x = xyDirection.x;
        moveDirection.z = xyDirection.z;
            

            //moveDirection = transform.right * Input.GetAxis("Horizontal") + transform.forward * Input.GetAxis("Vertical");
            //moveDirection *= speed;
        //}
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.normal == Vector3.down) {
            moveDirection.y = 0;
        }
    }



}
