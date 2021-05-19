using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    [System.Serializable]
    enum Direction{
           X,
           Y,
           Z
    }

    [SerializeField]
    Direction movementDirection;
    [SerializeField]
    float amplitude;

    [SerializeField]
    float speedMultiplier;
    float timer;

    Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position;
    }



    private void Update()
    {
        Move();
    }

    private void Move()
    {
        timer += Time.deltaTime * speedMultiplier;

        if (movementDirection == Direction.X) {
            transform.position = startPosition + new Vector3(amplitude * Mathf.Sin(timer), 0, 0);
        }
        if (movementDirection == Direction.Y) {
            transform.position = startPosition + new Vector3(0, amplitude * Mathf.Sin(timer), 0);
        }
        if (movementDirection == Direction.Z){
            transform.position = startPosition + new Vector3(0, 0, amplitude * Mathf.Sin(timer));
        }
    }
}
