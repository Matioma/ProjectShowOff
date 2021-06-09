using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider))]
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
            Debug.Log(numPushingObjects);
        }
    }

    public void Decrement() {
        numPushingObjects--;
        Debug.LogWarning("Decreased");
    }

    private void Awake()
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        if (rigidbody != null) {
            rigidbody.useGravity = false;
            rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        }
    }


    private void OnTriggerEnter(Collider other)
    {

        PushableObject pushableObject = other.gameObject.GetComponent<PushableObject>();
        if (pushableObject != null) {
            pushableObject.SetPlatformTrigger(this);
            NumPushingObjects++;
        }

        //if ( other.gameObject.GetComponent<CharachterModel>())
        //{
        //    NumPushingObjects++;
        //}
    }


    private void OnTriggerExit(Collider other)
    {
        PushableObject pushableObject = other.gameObject.GetComponent<PushableObject>();
        if (pushableObject != null)
        {
            pushableObject.SetPlatformTrigger(null);
            NumPushingObjects--;
        }
        //if (other.gameObject.GetComponent<CharachterModel>())
        //{
        //    NumPushingObjects--;
        //}
    }

}
