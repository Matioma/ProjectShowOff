using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SphereCollider))]
public class CharachterModel : MonoBehaviour, ICharacterController
{
    protected CharacterController controller;
    [SerializeField]
    public UnityEvent onUseSkill;



    [Header("Movement")]
    [SerializeField]
    float acceletation = 10;
    [SerializeField]
    protected float speed = 6.0f;
    [SerializeField]
    float gravity = 20.0f;
    [Range(0,1)]
    public float drag = 0.8f;
    float currentSpeed = 0;


    protected Vector3 velocity = Vector3.zero;

    public void AddAceeleration(Vector3 velocity) {
        this.velocity.y = 0;
        this.velocity += velocity;
    }



    protected void Awake()
    {
        controller = GetComponent<CharacterController>();
    }


    private void FixedUpdate()
    {
        velocity.y -= gravity * Time.deltaTime;
        controller.Move(velocity* Time.deltaTime);


        float y = velocity.y;
        velocity *= drag;
        velocity.y = y;
    }



    public virtual void SpecialAction() {
    }
    public virtual void ReleaseSpecialAction(){
    }
    public void Move(Vector3 direction)
    {
        float yVelocity = velocity.y;

        velocity.y = 0;
        velocity += direction * acceletation;

        //Add new Acceleration
        if (velocity.sqrMagnitude > speed * speed) {
            velocity = velocity.normalized * speed;
        }
        velocity.y = yVelocity;
    } 

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.normal == Vector3.down) {
            velocity.y = 0;
        }
    }

 
}
