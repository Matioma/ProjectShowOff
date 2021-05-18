using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider))]
public class Geyzer : MonoBehaviour
{

    public UnityEvent onGayzerExplode;

    [SerializeField]
    float explosionDelay;
    [SerializeField]
    float rabbitUpAcceleration;
    [SerializeField]
    float otherAnimalsUpAcceleration;

    List<CharachterModel> characterModels = new List<CharachterModel>();

    private void Start()
    {
        InvokeRepeating("PushCharacters", explosionDelay, explosionDelay);
    }

    void PushCharacters() {
        onGayzerExplode?.Invoke();
        foreach (var character in characterModels) {
            if (character is RabbitController)
            {
                character.AddAceeleration(new Vector3(0, rabbitUpAcceleration, 0));
            }
            else
            {
                character.AddAceeleration(new Vector3(0, otherAnimalsUpAcceleration, 0));
            }
        }
    }



 
    private void OnTriggerEnter(Collider other)
    {
        CharachterModel charachterModel = other.GetComponent<CharachterModel>();
        if (charachterModel != null) {
            characterModels.Add(charachterModel);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        CharachterModel charachterModel = other.GetComponent<CharachterModel>();
        if (charachterModel != null)
        {
            characterModels.Remove(charachterModel);
        }
    }
}
