using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxController : CharachterModel
{
    public float dashTime = 1;
    public float dashSpeed = 8.0f;

    bool isDashing = false;



    public override void SpecialAction()
    {
        if (!isDashing) { 
            StartCoroutine(Dash());
        }
    }

    IEnumerator Dash()
    {
        isDashing = true;
        Vector3 lastDirection = moveDirection;
        lastDirection.y = 0;
        lastDirection.Normalize();
        float startTime = Time.time;
        while (Time.time < startTime + dashTime) {
            lastDirection.y = 0.00f;
            moveDirection.y = 0;
            controller.Move(lastDirection * dashSpeed * Time.deltaTime);
            yield return null;
        }
        isDashing = false;
    }


    IEnumerator dashFinished() {
        isDashing = false;
        yield return null;
    }
}
