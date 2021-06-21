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
    bool isBigBox;
    public bool IsBigBox { get { return isBigBox; } } 



    [SerializeField]
    UnityEvent OnStartDragging;

    [SerializeField]
    UnityEvent OnStopDragging;

    public bool IsBeingGrabbed() {
        return transform.parent != initialParent;
    }


    PlatformTrigger platformTrigger;
    public void SetPlatformTrigger(PlatformTrigger platform) {
        this.platformTrigger = platform;
    }
    public PlatformTrigger GetPlatformTrigger() {
        return platformTrigger;
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


    public void FreezePosition(){
        rigidbody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
    }

    public void UnFreezePosition()
    {
        rigidbody.constraints = RigidbodyConstraints.None;
    }


    public Transform GetInitialParent{
        get{ return initialParent; }
    }
    
    void Start()
    {
        initialParent = transform.parent;
        rigidbody = GetComponent<Rigidbody>();
        FreezePosition();
    }


    private void Update()
    {
        if (IsBeingGrabbed()) {
            IsDragged = transform.parent.GetComponent<CharacterController>().velocity.sqrMagnitude > 0.1f;
        }
        else {
            IsDragged = false;
            //if (rigidbody != null) { 
            //    IsDragged = rigidbody.velocity.sqrMagnitude > 0;
            //}
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
