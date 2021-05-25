using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[RequireComponent(typeof(BoxCollider))]
public class CheckPoint : MonoBehaviour
{
    UnityEvent onCheckPointReached;
    Player playerModel;

    int charactersInZone = 0;

    private void Awake()
    {
        playerModel = FindObjectOfType<Player>();
    }
    void CharacterEnterZone() {
        charactersInZone++;
        if (charactersInZone >= playerModel.ControlledCharacters.Count) {
            Debug.Log("SavePoint reached");
            onCheckPointReached?.Invoke();
        }
    }
    void CharacterExitZone() {
        charactersInZone--;
    }
    private void OnTriggerEnter(Collider other)
    {
        foreach (var character in playerModel.ControlledCharacters) {
            if (other.gameObject == character.gameObject) {
                CharacterEnterZone();
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        foreach (var character in playerModel.ControlledCharacters)
        {
            if (other.gameObject == character.gameObject)
            {
                CharacterExitZone();
            }
        }
    }
}
