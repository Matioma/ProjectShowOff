using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class Destroyable : MonoBehaviour
{
    [SerializeField]
    UnityEvent onDestroying;

    public void onCollisionEvent() {
        Debug.Log("Cool");
        onDestroying?.Invoke();
        Destroy(this.gameObject, 0.1f);
    }

   
    
}
