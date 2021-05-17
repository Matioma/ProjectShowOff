using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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

    //bool triggerPressed;
    //bool TriggerPressed
    //{
    //    get { return triggerPressed; }

    //    set {
    //        if (triggerPressed == value) return;
    //        if (value) onPressTrigger?.Invoke(); // if new value is true
    //        if (!value) onReleaseTrigger?.Invoke(); // if new value is false
    //        triggerPressed = value; 
    //    }
    //}

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.GetComponent<PushableObject>() || 
            collision.gameObject.GetComponent<CharachterModel>()) {
            NumPushingObjects++;
        }
        Debug.Log(NumPushingObjects);
    }

    private void OnCollisionExit(Collision collision)
    {
  
        if (collision.gameObject.GetComponent<PushableObject>() ||
           collision.gameObject.GetComponent<CharachterModel>())
        {
            NumPushingObjects--;
        }
        Debug.Log(NumPushingObjects);
    }
}
