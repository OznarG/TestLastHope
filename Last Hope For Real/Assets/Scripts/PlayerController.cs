using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    private CharacterController controller;
    [SerializeField] private Transform cameraThird;

    [Header("Movement Settings")]
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float spritSpeed = 10f;
    [SerializeField] private float spritAcceleration = 5f;
    [SerializeField] private float turningSpeed = 2f;
    [SerializeField] private float gravity = 9.81f;
    [SerializeField] private float jumpHeight = 0.2f;


    private float verticalVelovity;
    private float speed;

    [Header("Input")]
    private float moveInput;
    private float turnInput;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }
    private void Update()
    {
        InputManagement();
        Movement();
        Turn();
    }

    private void Movement()
    {
        GroundMovement();
    }
    private void GroundMovement()
    {
        Vector3 move = new Vector3(turnInput, 0, moveInput);
        move = cameraThird.transform.TransformDirection(move);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = Mathf.Lerp(speed, spritSpeed, spritAcceleration * Time.deltaTime);
        }
        else
        {
            speed = Mathf.Lerp(speed, walkSpeed, spritAcceleration * Time.deltaTime);
        }
        move.y = VerticalForceCalculation();
        move *= speed;

        controller.Move(move * Time.deltaTime);
    }
    private void Turn()
    {
        if (Mathf.Abs(turnInput) > 0 || Mathf.Abs(moveInput) > 0)
        {
            Vector3 currentLookDirection = controller.velocity.normalized;
            currentLookDirection.y = 0;
            currentLookDirection.Normalize();

            Quaternion targetRotation = Quaternion.LookRotation(currentLookDirection);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * turningSpeed);
        }
    }
    private float VerticalForceCalculation()
    {
        if (controller.isGrounded)
        {
            verticalVelovity = -1f;
            if (Input.GetButtonDown("Jump"))
            {
                verticalVelovity = MathF.Sqrt(jumpHeight * gravity * 2);
            }
        }
        else
        {
            verticalVelovity -= gravity * Time.deltaTime;
        }
        return verticalVelovity;
    }
    private void InputManagement()
    {
        moveInput = Input.GetAxis("Vertical");
        turnInput = Input.GetAxis("Horizontal");
    }
}
