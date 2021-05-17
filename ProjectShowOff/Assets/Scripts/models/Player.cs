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


    [SerializeField]
    List<CharachterModel> characterControllers;
    int selectedCharacterId = 0;


   
    //CharachterModel controlledCharacter;

    [SerializeField]
    public UnityEvent onSwitchCharacter;
    public CharachterModel ControlledCharacter { 
        get { return characterControllers[selectedCharacterId]; } 
        private set { } 
    }

    private void Start()
    {
        //controlledCharacter = rabbitController;
    }


    public void SwitchCharacter() {
        //controlledCharacter.Move(Vector3.zero);
        ControlledCharacter.Move(Vector3.zero);
        selectedCharacterId = (selectedCharacterId + 1) % characterControllers.Count;
        

        //if(controlledCharacter == rabbitController)
        //{
        //    controlledCharacter = bearController;
        //}
        //else if (controlledCharacter == bearController) {
        //    controlledCharacter = rabbitController;
       
        //}
        onSwitchCharacter?.Invoke();
    }
}
