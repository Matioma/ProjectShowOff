using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharachterModel : MonoBehaviour, ICharacterController
{
    protected CharacterController controller;

    [SerializeField]
    protected Animator animator;


    public UnityEvent OnLanding;
    public UnityEvent onUseSkill;
    public UnityEvent OnWalking;
    public UnityEvent OnStanding;

    public UnityEvent onSelected;
    public UnityEvent onDiselected;


    bool skillsIsEnabled = true;
    public bool SkillIsEnabled
    {
        get
        {
            return skillsIsEnabled;
        }
        private set {
            skillsIsEnabled = value;
        }
    }

    public void CharacterSeleted() {
        onSelected?.Invoke();
    }
    public void CharacterDeselected()
    {
        onDiselected?.Invoke();
    }

    public void DisableSkills() {
        SkillIsEnabled = false;
    }
    public void EnableSkills() {
        SkillIsEnabled = true;
    }

    [Header("movement")]
    [SerializeField]
    float acceletation = 10;
    [SerializeField]
    protected float speed = 6.0f;

    public void SetSpeed(float speed) {
        if (speed > 0) {
            this.speed = speed;
        } else {
            this.speed = 0;
        }
    }

    public void PushBack( Vector3 velocity) {
        this.velocity = velocity;
        //controller.Move(velocity);

    }

    public void Slow(float percentage)
    {
        float percentSlow = percentage / 100.0f;
        speed = speed * (1 - percentSlow);
    }
    public void ReverseSlow(float percentage) {
        float percentSlow = percentage / 100.0f;
        speed = speed / (1 - percentSlow);
    }

    [SerializeField]
    float gravity = 20.0f;

    [SerializeField]
    [Range(0, 1)]
    [Tooltip("the higher the value the more flat should be the surface for landing")]
    float surfaceTolerance;

    [Range(0, 1)]
    public float drag = 0.8f;


    [SerializeField]
    [Range(0, 90)]
    float maxAngleFromUp = 50;


    protected Vector3 velocity = Vector3.zero;

    bool wasInAir = false;

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
        if (wasInAir && controller.isGrounded)
        {
            wasInAir = false;
            OnLanding?.Invoke();
        }
        else if (!controller.isGrounded) {
            wasInAir = true;
        }

        Vector3 XZVelocity = new Vector3(velocity.x, 0, velocity.z);


        //if (animator != null) animator.SetBool("IsMoving", XZVelocity.sqrMagnitude > 0);
        if (animator != null) animator.SetFloat("Velocity", XZVelocity.sqrMagnitude);

        if (XZVelocity.sqrMagnitude > 0)
        {
            OnWalking?.Invoke();//If Walking
        }
        else {
            OnStanding?.Invoke();
        }

        velocity.y -= gravity * Time.deltaTime; // gravity Acceleration
        controller.Move(velocity * Time.deltaTime);

        checkSlope();

        //Add XY drag
        float y = velocity.y;
        velocity *= drag;
        velocity.y = y;
    }



    public virtual void SpecialAction() {
        onUseSkill?.Invoke();
    }
    public virtual void ReleaseSpecialAction() {
    }


    void Update() {

    }

    public void Move(Vector3 pDirection)
    {
        Vector3 localDirection = transform.forward * pDirection.z + transform.right * pDirection.x;

        float yVelocity = velocity.y;
        velocity.y = 0;
        velocity += localDirection * acceletation;

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



    void checkSlope (){
        RaycastHit ray;

        if (Physics.Raycast(transform.position, Vector3.down, out ray, 10)) {

            Debug.DrawRay(transform.position, ray.normal, Color.green);
            Vector3 right = Vector3.Cross(ray.normal, Vector3.up);
            Vector3 slideDirection = Vector3.Cross(ray.normal, right);
            Debug.DrawRay(transform.position, slideDirection, Color.green);
            //Debug.Log(maxAngleFromUp);
            //Debug.Log(canStand(ray.normal, maxAngle));
            if (canStand(ray.normal, maxAngleFromUp))
            {
            }
            else {
              

                Vector3 newVelocity = new Vector3(velocity.x, velocity.y, velocity.z);
                float newSlidingVelocity = Vector3.Dot(newVelocity, slideDirection);
                Debug.Log(velocity);

                if (controller.isGrounded) {
                    Debug.Log(slideDirection * newSlidingVelocity);

                    velocity = slideDirection * newSlidingVelocity;
                }
            }
        }
    }


    bool canStand(Vector3 normal, float angleLimit) {

        //Debug.Log(Vector3.Dot(normal, Vector3.up));
        //Debug.Log(Vector3.Dot(normal, Vector3.up) > Mathf.Cos(angleLimit));


        //(Vector3.Dot(normal, Vector3.up) > tolerance)
        //Debug.Log();

        //Debug.Log(Mathf.Cos(maxAngleFromUp));


        return Vector3.Dot(normal, Vector3.up) > Mathf.Cos(angleLimit);
    }
}
