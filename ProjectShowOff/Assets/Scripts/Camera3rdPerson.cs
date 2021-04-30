using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera3rdPerson : MonoBehaviour
{
    [SerializeField]
    Transform targetBody;

    [SerializeField]
    float rotationSensitivity;

    [Range(0, 1)]
    [SerializeField]
    float followSensitivity;

    Vector3 offset;


    float xRotation = 0f;
    float yRotation = 0f;


    private void Start()
    {
        offset = transform.position - targetBody.position;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        xRotation = 0;
        yRotation = 0;
        float mouseX = Input.GetAxis("Mouse X") * rotationSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * rotationSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        yRotation += mouseX;
        yRotation = Mathf.Clamp(yRotation, -90f, 90f);


        //Rotate the camera around the target
        transform.RotateAround(targetBody.position, Vector3.up, yRotation);
        
        
        //Compute the new rotated offset from the target
        Quaternion offsetRotationQuaternion = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
        transform.position = Vector3.Lerp(transform.position, targetBody.position + offsetRotationQuaternion*offset, 1);

        //Make the target rotate in camera Direction
        Vector3 lookDirectionTarget = targetBody.position + transform.forward;
        lookDirectionTarget.y = targetBody.position.y;
        targetBody.LookAt(lookDirectionTarget);
       
    }

}
