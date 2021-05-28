using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class PushableObject : MonoBehaviour
{

    [SerializeField]
    [Tooltip("Increase to make it harder to push the object")]
    float drag;

    Rigidbody rigidbody;
    Transform initialParent;



    [SerializeField]
    UnityEvent OnStartDragging;

    [SerializeField]
    UnityEvent OnStopDragging;

    bool isBeingGrabbed() {
        return transform.parent != initialParent;
    }

    [SerializeField]
    bool isDragged = false;
    bool IsDragged {
        get {
            return isDragged;
        }
        set {
            if (value != isDragged) {
                if (value)
                {
                    OnStartDragging?.Invoke();
                }
                else {
                    OnStopDragging?.Invoke();
                }            
            }
            isDragged = value;
        }
    }


    public Transform GetInitialParent{
        get{ return initialParent; }
    }
    
    void Start()
    {
        initialParent = transform.parent;
        rigidbody = GetComponent<Rigidbody>();
    }


    private void Update()
    {
        if (isBeingGrabbed()) {
            IsDragged = transform.parent.GetComponent<CharacterController>().velocity.sqrMagnitude > 0;
        }
        else {

            if (rigidbody != null) { 
                IsDragged = rigidbody.velocity.sqrMagnitude > 0;
            }

        }


        //if (isBeingGrabbed())
        //{
        //    Debug.Log(transform.parent.GetComponent<CharacterController>().velocity);
        //}
        //else {
        //    if (rigidbody != null)
        //        Debug.Log(rigidbody.velocity);
        //}
        
    }

    private void OnValidate()
    {
        GetComponent<Rigidbody>().drag = drag;
    }
}
