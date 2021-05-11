using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRestricted : MonoBehaviour
{
    [SerializeField]
    Transform targetBody;

    [SerializeField]
    float rotationSensitivity;

    [Range(0, 1)]
    [SerializeField]
    float followSensitivity;

    Vector3 offset;

    [SerializeField]
    [Range(0, 90)]
    float maxRotationX;
    [SerializeField]
    [Range(0, 90)]
    float maxRotationY;




    float xRotation = 0f;
    float yRotation = 0f;


    Vector3 initialPosition;
    Quaternion initialRotation;


    private void Start()
    {
        offset = transform.position - targetBody.position;

        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    private void Update()
    {
        float deltaX = Input.GetAxis("Mouse X") * rotationSensitivity * Time.deltaTime;
        yRotation += deltaX;
        yRotation = Mathf.Clamp(yRotation, -maxRotationY, maxRotationY);

        float deltaY = Input.GetAxis("Mouse Y") * rotationSensitivity * Time.deltaTime;
        xRotation += deltaY;
        xRotation = Mathf.Clamp(xRotation, -maxRotationX, maxRotationX);


        if (deltaX !=0 || deltaY != 0) {
            transform.SetPositionAndRotation(initialPosition, initialRotation);
            transform.RotateAround(targetBody.position, Vector3.right, xRotation);
            transform.RotateAround(targetBody.position, Vector3.up, yRotation);
        }



        Quaternion offsetQuaternion = Quaternion.Euler(xRotation, yRotation, 0);
        transform.position = targetBody.position + offsetQuaternion*offset;
    }
}