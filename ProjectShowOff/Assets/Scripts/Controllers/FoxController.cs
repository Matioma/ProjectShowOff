using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxController : CharachterModel
{

    [Header("Skill Related")]
    public float dashTime = 1;
    public float dashSpeed = 8.0f;

    bool isDashing = false;



    public override void SpecialAction()
    {
        if (!SkillIsEnabled) return;
        if (!controller.isGrounded) return;
        onUseSkill?.Invoke();
        if (!isDashing) { 
            StartCoroutine(Dash());
        }
    }


    void SetAnimatorTriggerStartDashing() {
        animator.SetTrigger("StartDash");
    }

    void SetAnimatorTriggerEndDashing()
    {
        animator.SetTrigger("EndDash");
    }

    void SetBoolValue()
    {
        animator.SetBool("isDashing", isDashing);

    }


    IEnumerator Dash()
    {
        isDashing = true;
        SetBoolValue();
        //SetAnimatorTriggerStartDashing();
        Vector3 lastDirection = velocity;
        lastDirection.y = 0;
        lastDirection.Normalize();
        float startTime = Time.time;
        while (Time.time < startTime + dashTime) {
            lastDirection.y = 0.00f;
            velocity.y = 0;
            controller.Move(lastDirection * dashSpeed * Time.deltaTime);
            yield return null;
        }
        isDashing = false;
        //SetAnimatorTriggerEndDashing();
        SetBoolValue();
    }


    IEnumerator dashFinished() {
        isDashing = false;
        yield return null;
    }


    public void OnTriggerEnter(Collider other)
    {
        if (!isDashing) return;

        Destroyable destroyable = other.gameObject.GetComponent<Destroyable>();
        if (other.gameObject.GetComponent<Destroyable>())
        {
            destroyable.onCollisionEvent();
        }
    }

    //public void OnControllerColliderHit(ControllerColliderHit collision)
    //{
    //    if (!isDashing) return;

    //    Destroyable destroyable = collision.gameObject.GetComponent<Destroyable>();
    //    if (collision.gameObject.GetComponent<Destroyable>())
    //    {
    //        destroyable.onCollisionEvent();
    //    }
    //}
}
