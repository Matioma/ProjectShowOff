using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum RotationAxis
{
    X,
    Y,
    Z
}


public class Rotatng : MonoBehaviour
{
    [SerializeField]
    RotationAxis rotationAxis;
    [SerializeField]
    float amplitude;

    [SerializeField]
    float speedMultiplier;


    float timer;


    void Start()
    {
        
    }

    void Update()
    {
        Rotate();
    }



    void Rotate()
    {
        timer += Time.deltaTime * speedMultiplier;
        if (rotationAxis == RotationAxis.X)
        {
            transform.rotation = Quaternion.Euler(Mathf.Sin(timer)*amplitude,0,0);
        }
        if (rotationAxis == RotationAxis.Y)
        {
            transform.rotation = Quaternion.Euler(0, Mathf.Sin(timer) * amplitude, 0);
        }
        if (rotationAxis == RotationAxis.Z)
        {
            transform.rotation = Quaternion.Euler(0, 0, Mathf.Sin(timer) * amplitude);
        }
    }


}
