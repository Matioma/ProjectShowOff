using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharachterModel : MonoBehaviour, ICharacterController
{
    protected CharacterController controller;



    [Header("movement")]
    public float acceletation = 10;
    public float speed = 6.0f;
    public float gravity = 20.0f;

    [Range(0,1)]
    public float drag = 0.8f;

    float currentSpeed = 0;


    protected Vector3 velocity = Vector3.zero;



   // protected Vector3 velocity =Vector3.zero;

    public void AddAceeleration(Vector3 velocity) {
        this.velocity.y = 0;
        this.velocity += velocity;
    }



    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }


    private void FixedUpdate()
    {
        velocity.y -= gravity * Time.deltaTime;
        controller.Move(velocity* Time.deltaTime);
        //controller.Move(velocity * Time.deltaTime);
    }



    public virtual void SpecialAction() {}
    public virtual void ReleaseSpecialAction(){
    }


    public void Move(Vector3 direction)
    {
        float yVelocity = velocity.y;

        velocity.y = 0;
        velocity += direction * acceletation;
        if (direction.sqrMagnitude == 0)
        {
            velocity *= drag;
        }

        //Add new Acceleration
        if (velocity.sqrMagnitude > speed * speed) {
            velocity = velocity.normalized * speed;
        }
        velocity.y = yVelocity;


        ////IF no input
        //if (direction.sqrMagnitude == 0)
        //{
        //    currentSpeed = 0;
        //}

        //Set the new Direction
        //Vector3 xyDirection = direction * currentSpeed;
        //moveDirection.x = xyDirection.x;
        //moveDirection.z = xyDirection.z;

        ////Accelerate the player
        //currentSpeed += acceletation * Time.deltaTime;
        //if (currentSpeed > speed) currentSpeed = speed;
    } 

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.normal == Vector3.down) {
            velocity.y = 0;
        }
    }

 
}
