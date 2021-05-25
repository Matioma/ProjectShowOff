using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
public class PlatformTrigger : MonoBehaviour
{
    [SerializeField]
    UnityEvent onPressTrigger;
    [SerializeField]
    UnityEvent onReleaseTrigger;

    int numPushingObjects;
    int NumPushingObjects {
        get { return numPushingObjects; }
        set {
            if (numPushingObjects == value) return;
            
            if (numPushingObjects == 0 && value > 0) onPressTrigger?.Invoke();
            if (value == 0) onReleaseTrigger?.Invoke();
            numPushingObjects = value; 
        }
    }

    private void Awake()
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.useGravity = false;
        rigidbody.constraints = RigidbodyConstraints.FreezeAll;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PushableObject>() ||
           other.gameObject.GetComponent<CharachterModel>())
        {
            NumPushingObjects++;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<PushableObject>() ||
         other.gameObject.GetComponent<CharachterModel>())
        {
            NumPushingObjects--;
        }
    }

    //private void o(Collision collision)
    //{

    //    if (collision.gameObject.GetComponent<PushableObject>() || 
    //        collision.gameObject.GetComponent<CharachterModel>()) {
    //        NumPushingObjects++;
    //    }
    //    Debug.Log(NumPushingObjects);
    //}

    private void OnCollisionExit(Collision collision)
    {
        //if (collision.gameObject.GetComponent<PushableObject>() ||
        //   collision.gameObject.GetComponent<CharachterModel>())
        //{
        //    NumPushingObjects--;
        //}
        //Debug.Log(NumPushingObjects);
    }
}
