using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearController : CharachterModel
{

    [Header("Skill Related")]
    [SerializeField]
    float pushPower;

    [SerializeField]
    float moveSpeedWhenPushing = 4;
    float initialmovementSpeed;

    bool bearTriesToPush = false;


    PushableObject attachedPushable = null;


    private void animationGrabBox(PushableObject obj) {
        if (obj.IsBigBox)
        {
            animator.SetBool("GrabBigBox", true);
        }
        else
        {
            animator.SetBool("GrabSmallBox", true);
        }
    }

    protected override void RotateCharacterModelInVelocityDirection(Vector3 velocity)
    {
        if (characterMeshParent == null) return;

        //Debug.Log("cool")
        if (attachedPushable == null ) {
            if (velocity.sqrMagnitude <= 0) return;
            characterMeshParent.transform.localRotation = Quaternion.LookRotation(velocity, Vector3.up);
        }
    }

    private void animationReleaseBox() {
        animator.SetBool("GrabBigBox",false);
        animator.SetBool("GrabSmallBox", false);
    }


    private void Awake()
    {
        base.Awake();
        initialmovementSpeed = speed;
    }
    public override void SpecialAction()
    {
        if (!SkillIsEnabled) return;
        bearTriesToPush = true;
    }
    public override void ReleaseSpecialAction() {
        bearTriesToPush = false;
        DetachPushable();
    }
    void AttachPushable(PushableObject pushable) {
        animationGrabBox(pushable);

        pushable.transform.parent = transform;
        attachedPushable = pushable;
        attachedPushable.GetPlatformTrigger()?.Decrement();
        SetSpeed(moveSpeedWhenPushing);
        base.SpecialAction();
    }

    void DetachPushable()
    {
        if (attachedPushable == null) return;

        animationReleaseBox();

        attachedPushable.transform.parent = attachedPushable.GetInitialParent;
        attachedPushable.GetPlatformTrigger()?.Decrement();
        attachedPushable = null;

        SetSpeed(initialmovementSpeed);
    }



    void OnControllerColliderHit(ControllerColliderHit hit) {
        if (!bearTriesToPush) return;
        if (attachedPushable != null) return;


        PushableObject pushableObject = hit.gameObject.GetComponent<PushableObject>();
        if (pushableObject!=null) {
            AttachPushable(pushableObject);
        }
    }
}
