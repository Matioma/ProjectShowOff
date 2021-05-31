using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRestricted : MonoBehaviour
{
    [SerializeField]
    Transform targetBody;

    Vector3 targetPosition;


    [SerializeField]
    float rotationSensitivity;

    //[Range(0, 1)]
    //[SerializeField]
    //float followSensitivity;

    Vector3 initialOffset;

    Vector3 rotatedOffsetVector;





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


    [SerializeField]
    [Range(0,1)]
    float transitionTime = 1;
    float timer=0;

    [SerializeField]
    bool isTransitioning;


    Player playerModel;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        initialOffset = transform.position - targetBody.position;

        initialPosition = transform.position;
        initialRotation = transform.rotation;

        playerModel = FindObjectOfType<Player>();
        targetBody = playerModel.ControlledCharacter.transform;
        playerModel.onSwitchCharacter.AddListener(UpdateFollowedCharacter);
    }

    private void Update()
    {
        //if (!isRestricted) 
        //FreeCamera();
        //else 
        //RestrictedCamera();


        if (!isTransitioning)
        {
            FreeCamera();
            transform.position = Vector3.Lerp(transform.position, targetBody.position + rotatedOffsetVector, 1);
        }
        
        //transform.position = targetBody.position + rotatedOffsetVector;
        //transform.position = Vector3.Lerp(transform.position, targetPosition, 0.9f);
    }


    //void RestrictedCamera()
    //{
    //    float deltaX = Input.GetAxis("Mouse X") * rotationSensitivity * Time.deltaTime;
    //    yRotation += deltaX;

    //    yRotation = Mathf.Clamp(yRotation, -maxRotationY, maxRotationY);
    //    yRotation = Mathf.Clamp(yRotation, -maxRotationX, maxRotationX);


    //    float deltaY = Input.GetAxis("Mouse Y") * rotationSensitivity * Time.deltaTime;
    //    xRotation -= deltaY;
    //    xRotation = Mathf.Clamp(xRotation, -maxRotationX, maxRotationX);


    //    if (deltaX != 0 || deltaY != 0)
    //    {
    //        transform.SetPositionAndRotation(initialPosition, initialRotation);
    //        transform.RotateAround(targetBody.position, Vector3.right, xRotation);
    //        transform.RotateAround(targetBody.position, Vector3.up, yRotation);
    //    }
    //    Quaternion offsetQuaternion = Quaternion.Euler(xRotation, yRotation, 0);
     

    //    transform.position = targetBody.position + offsetQuaternion * offset;
    //}

    void FreeCamera() {
        float deltaX = Input.GetAxis("Mouse X") * rotationSensitivity * Time.deltaTime;
        yRotation += deltaX;

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


        rotatedOffsetVector = getNewCameraOffset(offsetQuaternion);

        Vector3 lookDirectionTarget = targetBody.position + transform.forward;
        lookDirectionTarget.y = targetBody.position.y;
        targetBody.LookAt(lookDirectionTarget);
    }

    void startTransition(Vector3 position) {
        timer = 0;
        StartCoroutine(transition(position));
    }

    IEnumerator transition(Vector3 position) {
        isTransitioning = true;
     
        while (timer < transitionTime) {
            timer += Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, position, timer / transitionTime);
            Debug.Log(timer / transitionTime);
            yield return null;
        }
        isTransitioning = false;
    }



    Vector3 getNewCameraOffset(Quaternion rotation) {
        Vector3 offsetDirection  = (rotation * initialOffset).normalized;


        RaycastHit hit;
        if (Physics.Raycast(targetBody.position, offsetDirection, out hit, Mathf.Infinity)) {
            Debug.DrawRay(targetBody.position, offsetDirection * hit.distance, Color.yellow,0.1f);

            Vector3 hitDirection = hit.point - targetBody.position;

            if (hitDirection.magnitude < initialOffset.magnitude && hitDirection.magnitude > 1) {
                return hitDirection;
            }
        }
        return rotation * initialOffset;
    }


    public void UpdateFollowedCharacter() {
        targetBody = playerModel.ControlledCharacter.transform;
        startTransition(targetBody.position + rotatedOffsetVector);
    }

    private void OnDestroy()
    {
        playerModel.onSwitchCharacter.RemoveListener(UpdateFollowedCharacter);
    }
}
