using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target; // The target the camera follows (e.g., player character)
    public Vector3 offset; // Offset from the target position
    public float smoothSpeed = 0.125f;
    public Vector3 fixedRotation;

    //for inputs and movement
    public float rotationSpeed = 5f;
    private bool isRotating = false;

    private void Start()
    {
        //fixedRotation = transform.eulerAngles;
        fixedRotation = offset;
    }
    void LateUpdate()
    {
        InputsManagement();

        Vector3 desiredPosition = target.position + fixedRotation;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        
        transform.LookAt(target.position);
    }

    private void InputsManagement()
    {
        if(Input.GetKey(KeyCode.Q))
        {
            //transform.RotateAround(target.position, Vector3.up, rotationSpeed * Time.deltaTime);
            fixedRotation = Quaternion.AngleAxis(-rotationSpeed * Time.deltaTime, Vector3.up) * fixedRotation;
            Debug.Log("Q is hit" + Input.GetKey(KeyCode.Q));
        }
        if (Input.GetKey(KeyCode.E))
        {
            //transform.RotateAround(target.position, -Vector3.up, rotationSpeed * Time.deltaTime);
            fixedRotation = Quaternion.AngleAxis(rotationSpeed * Time.deltaTime, Vector3.up) * fixedRotation;
            Debug.Log("Q is hit" + Input.GetKey(KeyCode.E));
        }
        
    }
}
