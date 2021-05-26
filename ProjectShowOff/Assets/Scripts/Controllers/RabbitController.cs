using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitController : CharachterModel
{

    [Header("Skill Related")]
    public float jumpSpeed = 8.0f;

    public override void SpecialAction()
    {
        
        if (controller.isGrounded)
        {
            onUseSkill?.Invoke();
            velocity.y = jumpSpeed;
        }
    }
}
