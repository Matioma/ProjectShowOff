using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{

    [SerializeField]
    List<CharachterModel> characterControllers;
    int selectedCharacterId = 0;

    [SerializeField]
    public UnityEvent onSwitchCharacter;
    public CharachterModel ControlledCharacter { 
        get { return characterControllers[selectedCharacterId]; } 
        private set { } 
    }
    public UnityEvent onDeath;


    public void Die() {
        Debug.Log("Die");
        onDeath?.Invoke();
    }

    public void SwitchCharacter() {
        ControlledCharacter.Move(Vector3.zero);
        selectedCharacterId = (selectedCharacterId + 1) % characterControllers.Count;
        
        onSwitchCharacter?.Invoke();
    }
}
