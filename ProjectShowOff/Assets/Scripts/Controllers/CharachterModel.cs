using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharachterModel : MonoBehaviour, ICharacterController
{
    protected CharacterController controller;

    [Tooltip("Reference to the mesh parent used for proper rotation")]
    [SerializeField]
    protected GameObject characterMeshParent;

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

    public void PushBack(Vector3 velocity) {
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


    [Range(0, 1)]
    public float drag = 0.8f;


    [SerializeField]
    [Range(0, 90)]
    float maxAngleFromUp = 20;
    [SerializeField]
    float slidingAccelerator = 2;


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

    void UpdateAnimatorVelocityComponent(float value) {
        if (animator != null) animator.SetFloat("Velocity", value);
        else {
            Debug.LogWarning("Animator reference is missing");
        }
    }

    void updateIsGroundedAnimation(bool value) {
        if (animator != null) animator.SetBool("IsGrounded", value);
        else
        {
            Debug.LogWarning("Animator reference is missing");
        }

    }

    virtual protected void RotateCharacterModelInVelocityDirection(Vector3 velocity) {
        if (characterMeshParent == null) return;

        if (velocity.sqrMagnitude <= 0) return;
        characterMeshParent.transform.localRotation = Quaternion.LookRotation(velocity, Vector3.up);
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
        //Debug.Log(XZVelocity + transform.name);

        updateIsGroundedAnimation(controller.isGrounded);
        UpdateAnimatorVelocityComponent(XZVelocity.sqrMagnitude);
        //if (animator != null) animator.SetFloat("Velocity",);

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
        
        RotateCharacterModelInVelocityDirection(new Vector3(pDirection.x, 0, pDirection.z));

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
                    //Debug.Log(slideDirection * newSlidingVelocity);

                    velocity = slideDirection * slidingAccelerator;
                }
            }
        }
    }

    protected bool canStand()
    {
        RaycastHit ray;
        Debug.DrawRay(transform.position, Vector3.down * 10, Color.yellow, 10.0f);


        if (Physics.Raycast(transform.position, Vector3.down, out ray, 10))
        {
            Debug.DrawRay(transform.position, Vector3.down * 10, Color.cyan, 10.0f);
            Debug.Log(canStand(ray.normal, maxAngleFromUp));
            return canStand(ray.normal, maxAngleFromUp);
        }
        else {
           
        }
        return false;
    }

    protected bool canStand(Vector3 normal, float angleLimit) {


        return Vector3.Dot(normal, Vector3.up) > Mathf.Cos(angleLimit);
    }
}
