using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField]
    CharachterModel rabbitController; 
    [SerializeField] 
    CharachterModel bearController;
   
    CharachterModel controlledCharacter;

    [SerializeField]
    public UnityEvent onSwitchCharacter;
    public CharachterModel ControlledCharacter { 
        get { return controlledCharacter; } 
        private set { } 
    }

    private void Start()
    {
        controlledCharacter = rabbitController;
    }


    public void SwitchCharacter() {
        controlledCharacter.Move(Vector3.zero);
        if(controlledCharacter == rabbitController)
        {
            controlledCharacter = bearController;
        }
        else if (controlledCharacter == bearController) {
            controlledCharacter = rabbitController;
       
        }
        onSwitchCharacter?.Invoke();
    }
}
