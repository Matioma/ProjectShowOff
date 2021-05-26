using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class Pickup : MonoBehaviour
{
    [SerializeField]
    int scorePerPickUp;

    [SerializeField]
    UnityEvent onPickedUp;


    CharachterModel[] characters;
    Player playerModel;

    private void Start()
    {
        characters = FindObjectsOfType<CharachterModel>();
        playerModel = FindObjectOfType<Player>();
        onPickedUp.AddListener(DestroySelf);
        onPickedUp.AddListener(AddScore);
    }

    void AddScore() {
        playerModel.AddScore(scorePerPickUp);
    }

    void DestroySelf() {
        //Destroy(this.gameObject);
    }

    void Update() {

    }

    private void OnTriggerEnter(Collider other)
    {
        foreach (var character in characters) {
            if (other.gameObject == character.gameObject) {
                onPickedUp?.Invoke();
            }
        }
    }

    private void OnDestroy()
    {
        onPickedUp.RemoveListener(DestroySelf);
        onPickedUp.RemoveListener(AddScore);
    }

}
