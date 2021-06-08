using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitController : CharachterModel
{
    [Header("Skill Related")]
    public float jumpSpeed = 8.0f;

    public override void SpecialAction()
    {
        if (!canStand()) return;
        if (!SkillIsEnabled) return;
        if (controller.isGrounded)
        {
            base.SpecialAction();
            velocity.y = jumpSpeed;
        }
    }
}
