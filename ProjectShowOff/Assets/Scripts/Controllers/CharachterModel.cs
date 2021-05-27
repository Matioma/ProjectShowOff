using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharachterModel : MonoBehaviour, ICharacterController
{
    protected CharacterController controller;

    [SerializeField]
    public UnityEvent onUseSkill;

    
    public UnityEvent OnLand;
    public UnityEvent OnUseSpell;
    public UnityEvent OnWalking;





    [Header("movement")]
    [SerializeField]
    float acceletation = 10;
    [SerializeField]
    protected float speed = 6.0f;

    [SerializeField]
    float gravity = 20.0f;

    [SerializeField]
    [Range(0, 1)]
    [Tooltip("the higher the value the more flat should be the surface for landing")]
    float surfaceTolerance; 

    [Range(0,1)]
    public float drag = 0.8f;


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
        Vector3 XYVelocity = new Vector3(velocity.x, 0, velocity.y);
        if (XYVelocity.sqrMagnitude > 0) {
            OnWalking?.Invoke();//If Walking
        }

        velocity.y -= gravity * Time.deltaTime; // gravity Acceleration
        controller.Move(velocity* Time.deltaTime); 


        //Add XY drag
        float y = velocity.y;
        velocity *= drag;
        velocity.y = y;
    }



    public virtual void SpecialAction() {
        onUseSkill?.Invoke();
    }
    public virtual void ReleaseSpecialAction(){
    }


    void Update() {
        
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
        if (canStand(hit.normal, surfaceTolerance)){
            OnLand?.Invoke();
            velocity.y = 0;
        }
        else {
            velocity += hit.normal;
        }
        
    }


    bool canStand(Vector3 normal, float tolerance) {
        return (Vector3.Dot(normal, Vector3.up) > tolerance) ;
    }
}
