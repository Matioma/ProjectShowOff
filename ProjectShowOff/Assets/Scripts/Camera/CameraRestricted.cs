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
    bool isRestricted;

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


    Player playerModel;

    private void Start()
    {
        offset = transform.position - targetBody.position;

        initialPosition = transform.position;
        initialRotation = transform.rotation;

        playerModel = FindObjectOfType<Player>();
        targetBody = playerModel.ControlledCharacter.transform;
        playerModel.onSwitchCharacter.AddListener(UpdateFollowedCharacter);
    }

    private void Update()
    {

        if (!isRestricted) FreeCamera();
        else RestrictedCamera();
    }


    void RestrictedCamera()
    {
        float deltaX = Input.GetAxis("Mouse X") * rotationSensitivity * Time.deltaTime;
        yRotation += deltaX;

        yRotation = Mathf.Clamp(yRotation, -maxRotationY, maxRotationY);
        yRotation = Mathf.Clamp(yRotation, -maxRotationX, maxRotationX);


        float deltaY = Input.GetAxis("Mouse Y") * rotationSensitivity * Time.deltaTime;
        xRotation -= deltaY;
        xRotation = Mathf.Clamp(xRotation, -maxRotationX, maxRotationX);


        if (deltaX != 0 || deltaY != 0)
        {
            transform.SetPositionAndRotation(initialPosition, initialRotation);
            transform.RotateAround(targetBody.position, Vector3.right, xRotation);
            transform.RotateAround(targetBody.position, Vector3.up, yRotation);
        }
        Quaternion offsetQuaternion = Quaternion.Euler(xRotation, yRotation, 0);
        transform.position = targetBody.position + offsetQuaternion * offset;
    }

    void FreeCamera() {
        float deltaX = Input.GetAxis("Mouse X") * rotationSensitivity * Time.deltaTime;
        yRotation += deltaX;

        //yRotation = Mathf.Clamp(yRotation, -maxRotationY, maxRotationY);


        float deltaY = Input.GetAxis("Mouse Y") * rotationSensitivity * Time.deltaTime;
        xRotation -= deltaY;
        xRotation = Mathf.Clamp(xRotation, -maxRotationX, maxRotationX);


        if (deltaX != 0 || deltaY != 0)
        {
            transform.SetPositionAndRotation(initialPosition, initialRotation);
            transform.RotateAround(targetBody.position, Vector3.right, xRotation);
            transform.RotateAround(targetBody.position, Vector3.up, yRotation);
        }
        Quaternion offsetQuaternion = Quaternion.Euler(xRotation, yRotation, 0);
        transform.position = targetBody.position + offsetQuaternion * offset;


        Vector3 lookDirectionTarget = targetBody.position + transform.forward;
        lookDirectionTarget.y = targetBody.position.y;
        targetBody.LookAt(lookDirectionTarget);
    }


    public void UpdateFollowedCharacter() {
        targetBody = playerModel.ControlledCharacter.transform;
    }

    private void OnDestroy()
    {
        playerModel.onSwitchCharacter.RemoveListener(UpdateFollowedCharacter);
    }
}
