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


    PushableObject attachedPushable =null;

    private void Awake()
    {
        base.Awake();
        initialmovementSpeed = speed;
    }



    public override void SpecialAction()
    {
        base.SpecialAction();
        bearTriesToPush = true;
    }

    public override void ReleaseSpecialAction() {
        bearTriesToPush = false;
        DetachPushable();
    }

    void AttachPushable(PushableObject pushable) {
        pushable.transform.parent = transform;
        attachedPushable = pushable;
        speed = moveSpeedWhenPushing;
        onUseSkill?.Invoke();
    }

    void DetachPushable()
    {
        if (attachedPushable == null) return;
        attachedPushable.transform.parent = attachedPushable.GetInitialParent;
        attachedPushable = null;
        speed = initialmovementSpeed;
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
