using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PushableObject : MonoBehaviour
{
    [SerializeField]
    float mass;

    [SerializeField]
    [Tooltip("Increase to make it harder to push the object")]
    float drag;

    Rigidbody rigidbody;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.mass = mass;
        rigidbody.drag = drag;
        rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
    }
}
