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
    

      







        //xRotation = 0;
        //yRotation = 0;
        //float mouseX = Input.GetAxis("Mouse X") * rotationSensitivity * Time.deltaTime;
        //float mouseY = Input.GetAxis("Mouse Y") * rotationSensitivity * Time.deltaTime;

        //xRotation -= mouseY;
        //xRotation = Mathf.Clamp(xRotation, -90f, 90f);


        //yRotation += mouseX;
        //yRotation = Mathf.Clamp(yRotation, -90f, 90f);

        ////Debug.Log(yRotation);
        //CurrentRotationY += yRotation;
        //CurrentRotationX += xRotation;


        //if (CurrentRotationY < maxRotationY && CurrentRotationY > -maxRotationY)
        //{
        //    transform.RotateAround(targetBody.position, Vector3.up, yRotation);

        //    Quaternion offsetRotationQuaternion = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
        //    transform.position = Vector3.Lerp(transform.position, targetBody.position + offsetRotationQuaternion * offset, 1);
        //}





        //if (CurrentRotationX < maxRotationX && CurrentRotationY > -maxRotationX)
        //{
        //    transform.RotateAround(targetBody.position, Vector3.right, xRotation);
        //    //transform.RotateAround(targetBody.position, Vector3.right, xRotation);

        //    Quaternion offsetRotationQuaternion = Quaternion.Euler(transform.rotation.eulerAngles.x, 0, 0);
        //    transform.position = Vector3.Lerp(transform.position, targetBody.position + offsetRotationQuaternion * offset, 1);
        //}










        //Debug.Log(transform.rotation.eulerAngles.x);

        //Quaternion offsetRotationQuaternion = Quaternion.Euler(0, Mathf.Clamp(transform.rotation.eulerAngles.y, -maxRotationY, maxRotationY), 0);
        //Debug.Log(Mathf.Clamp(transform.rotation.eulerAngles.y, -maxRotationY, maxRotationY));


        //transform.position = Vector3.Lerp(transform.position, targetBody.position + offsetRotationQuaternion * offset, 1);

        //transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, Mathf.Clamp(transform.rotation.y, -maxRotationY, maxRotationY), transform.rotation.eulerAngles.z);
        //transform.position = Vector3.Lerp(transform.position, targetBody.position + offset, 1);


        //if (CurrentRotationY < maxRotationY && CurrentRotationY > -maxRotationY) {

        //    transform.RotateAround(targetBody.position, Vector3.up, yRotation);

        //    Quaternion offsetRotationQuaternion = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
        //    transform.position = Vector3.Lerp(transform.position, targetBody.position + offsetRotationQuaternion * offset, 1);
        //}


        //transform.RotateAround(targetBody.position, Vector3.up, yRotation);

        //Quaternion offsetRotationQuaternion = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
        //transform.position = Vector3.Lerp(transform.position, targetBody.position + offsetRotationQuaternion * offset, 1);



        ////currentRotationX -= xRotation;
        //currentRotationY -= mouseX;
        //currentRotationY = Mathf.Clamp(currentRotationY, -maxRotationX, maxRotationX);

        //if (currentRotationX > maxRotationX || currentRotationX < -maxRotationX)
        //{

        //}
        //else {
        //    transform.RotateAround(targetBody.position, Vector3.up, yRotation);

        //    Quaternion offsetRotationQuaternion = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
        //    transform.position = Vector3.Lerp(transform.position, targetBody.position + offsetRotationQuaternion * offset, 1);
        //}

        //transform


        //Rotate the camera around the target
        //transform.RotateAround(targetBody.position, Vector3.up, yRotation);


        //////Compute the new rotated offset from the target
        //Quaternion offsetRotationQuaternion = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
        //transform.position = Vector3.Lerp(transform.position, targetBody.position + offsetRotationQuaternion * offset, 1);



        //Make the target rotate in camera Direction
        //Vector3 lookDirectionTarget = targetBody.position + transform.forward;
        //lookDirectionTarget.y = targetBody.position.y;
        //targetBody.LookAt(lookDirectionTarget);

    }
}
