using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(BoxCollider))]
public class CheckPoint : MonoBehaviour
{
    [SerializeField]
    UnityEvent onCheckPointReached;
    Player playerModel;

    int charactersInZone = 0;

    [SerializeField]
    string targetScene;
    


    private void Awake()
    {
        playerModel = FindObjectOfType<Player>();

        onCheckPointReached.AddListener(SaveProgress);
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


    void SaveProgress()
    {
        SceneManager.LoadScene(targetScene);
    }


}
