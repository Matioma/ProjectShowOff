using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    WASDController rabbitController; 
   
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
}
