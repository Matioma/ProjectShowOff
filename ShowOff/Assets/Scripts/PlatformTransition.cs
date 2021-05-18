using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformTransition : MonoBehaviour
{
    [SerializeField]
    public Vector3 targetPosition;

    [SerializeField]
    float animationLength;

    public void StartTransition() {
        StartCoroutine(Transition());
    }

    IEnumerator Transition() {
        Vector3 startPosition = transform.position;
        float startTime = Time.time;

        while (Time.time < startTime + animationLength) {

            float progressPercent = (Time.time - startTime) / animationLength;
            Debug.Log(progressPercent);
            transform.position = Vector3.Lerp(startPosition, targetPosition, progressPercent);
            yield return null;
        }
    }
}
