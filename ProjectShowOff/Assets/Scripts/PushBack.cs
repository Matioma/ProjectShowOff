using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider))]
public class PushBack : MonoBehaviour
{
    [SerializeField]
    float pushBackVelocity;
    [SerializeField]
    float pushBackTime= 1;

    [SerializeField]
    UnityEvent onPushTrigger;


    private void OnTriggerEnter(Collider other)
    {
        Vector3 pushBackDirection = (other.gameObject.transform.position - transform.position).normalized;

        CharachterModel charachter = other.GetComponent<CharachterModel>();

        if (charachter != null)
        {
            //charachter.AddAceeleration(pushBackDirection * pushBackVelocity);
            StartCoroutine(Pushback(charachter, pushBackDirection));
            onPushTrigger?.Invoke();
        }
    }


    IEnumerator Pushback(CharachterModel character,Vector3 direction)
    {
        float startTime = Time.time;
        while (Time.time < startTime + pushBackTime)
        {
            character.AddAceeleration(direction * pushBackVelocity);
            yield return null;
        }
    }
}
