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


        Debug.Log(yRotation);


        transform.RotateAround(targetBody.position, Vector3.up, yRotation);
        targetBody.Rotate(Vector3.up, yRotation);

        Quaternion offsetRotationQuaternion = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);


        transform.position =Vector3.Lerp(transform.position, targetBody.position + offsetRotationQuaternion * offset, followSensitivity);



        //transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        //targetBody.Rotate(Vector3.up * mouseX);

        //float yRotation = mouseX * rotationSensitivity;


        //Vector3 rotation = transform.rotation.eulerAngles;


        //transform.rotation = Quaternion.Euler(rotation.x, rotation.y + yRotation, rotation.z);
        //Vector3 newoffset = transform.rotation * offset;

        //transform.position = targetBody.position + newoffset;
        
    }

}
