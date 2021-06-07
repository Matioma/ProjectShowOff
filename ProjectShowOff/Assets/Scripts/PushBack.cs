using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class PushBack : MonoBehaviour
{
    [SerializeField]
    float pushBackVelocity;


    private void OnTriggerEnter(Collider other)
    {
        Vector3 pushBackDirection = (other.gameObject.transform.position - transform.position).normalized;

        CharachterModel charachter = other.GetComponent<CharachterModel>();

        if (charachter != null) {
            charachter.AddAceeleration(pushBackDirection * pushBackVelocity);
        }
    }
}
