using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearController : WASDController
{
    [SerializeField]
    float pushPower;

    void OnControllerColliderHit(ControllerColliderHit hit) {
        Rigidbody body = hit.collider.attachedRigidbody;

        if (body == null || body.isKinematic) {
            return;
        }
        if (hit.moveDirection.y < -0.3) {
            return;
        }

        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);

        body.velocity = pushDir * pushPower;
    }
}