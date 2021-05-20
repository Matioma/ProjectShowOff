using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitController : CharachterModel
{

    public float jumpSpeed = 8.0f;

    public override void SpecialAction()
    {
        if (controller.isGrounded)
        {
            velocity.y = jumpSpeed;
        }
    }
}
