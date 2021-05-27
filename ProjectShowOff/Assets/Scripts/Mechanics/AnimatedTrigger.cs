using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedTrigger : MonoBehaviour
{
    [SerializeField]
    Animator animator;
    public void TriggerAnimation(string triggerName) {
        animator.SetTrigger(triggerName);
    }


    public void ActivateBoolValue(string boolValueName) {
        animator.SetBool(boolValueName, true);
    }

    public void DisactivateBoolValue(string boolValueName) {
        animator.SetBool(boolValueName, false);
    }
  
}
