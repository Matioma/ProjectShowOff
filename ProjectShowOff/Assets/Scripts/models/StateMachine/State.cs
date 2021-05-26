using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



[CreateAssetMenu(fileName = "State", menuName = "StateMachine/State", order = 1)]
public class State : ScriptableObject
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

    }

    public void UpdateState() { 
        
    }
    public void Exit() { 
    }
}
