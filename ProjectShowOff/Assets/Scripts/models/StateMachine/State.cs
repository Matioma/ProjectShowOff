using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class State
{
    [SerializeField]
    UnityEvent onStateEnter;
    [SerializeField]
    UnityEvent onStateUpdate;
    [SerializeField]
    UnityEvent onStateExit;

    [SerializeField]
    State targetState;
    public void Enter() {
        onStateEnter?.Invoke();
    }

    public void UpdateState() {
        onStateUpdate?.Invoke();
    }
    public void Exit() {
        onStateExit?.Invoke();
    }
}
