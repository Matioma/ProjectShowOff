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

  
}
