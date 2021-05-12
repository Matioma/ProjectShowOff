using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    WASDController rabbitController; 
    [SerializeField] 
    WASDController bearController;
   
    [SerializeField]
    ICharacterController controlledCharacter;

    public ICharacterController ControlledCharacter { 
        get { return controlledCharacter; } 
        private set { } 
    }

    private void Start()
    {
        controlledCharacter = rabbitController;
    }


    public void SwitchCharacter() {
        controlledCharacter.Move(Vector3.zero);
        if(controlledCharacter == (ICharacterController)rabbitController)
        {

            controlledCharacter = bearController;
            return;
        }
        if (controlledCharacter == (ICharacterController)bearController) {
            controlledCharacter = rabbitController;
            return;
        }
    }
}
