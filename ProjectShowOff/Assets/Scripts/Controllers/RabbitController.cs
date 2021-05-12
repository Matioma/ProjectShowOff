using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitController : WASDController
{

    public float jumpSpeed = 8.0f;

    public override void SpecialAction()
    {
        if (controller.isGrounded)
        {
            moveDirection.y = jumpSpeed;
        }
    }
}
