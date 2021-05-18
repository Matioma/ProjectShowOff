using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent (typeof(Rigidbody))]
public class DangerousObstacle : MonoBehaviour
{
    Player playerModel;
    Rigidbody rigidbody;

    [SerializeField]
    UnityEvent onCollisionWithCharacter;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        playerModel = FindObjectOfType<Player>();
        if (playerModel == null) Debug.LogWarning("The scene does not have Player Component attached to any object");

        rigidbody.useGravity = false;
        rigidbody.constraints = RigidbodyConstraints.FreezeAll;
    }


    private void OnCollisionEnter(Collision collision)
    {
        CharachterModel charachterModel = collision.gameObject.GetComponent<CharachterModel>();
        if (charachterModel != null) {
            onCollisionWithCharacter?.Invoke();
            playerModel?.Die();
        }
    }


}
