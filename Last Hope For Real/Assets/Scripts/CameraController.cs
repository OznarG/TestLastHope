using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target; // The target the camera follows (e.g., player character)
    public Vector3 offset; // Offset from the target position
    public float smoothSpeed = 0.125f;
    public Vector3 fixedRotation;

    private void Start()
    {
        fixedRotation = transform.eulerAngles;
    }
    void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
        
    }
}
