using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PushableObject : MonoBehaviour
{

    [SerializeField]
    [Tooltip("Increase to make it harder to push the object")]
    float drag;

    Rigidbody rigidbody;

    Transform initialParent;
    public Transform GetInitialParent{
        get{ return initialParent; }
    }
    
    void Start()
    {
        initialParent = transform.parent;
        //rigidbody = GetComponent<Rigidbody>();
        //rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
    }

    private void OnValidate()
    {
        //Debug.Log(drag);
        GetComponent<Rigidbody>().drag = drag;
    }
}
