using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IDamage
{
    [Header("References")]
    public  CharacterController controller;
    [SerializeField] private Transform cameraThird;

    [Header("Movement Settings")]
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float spritSpeed = 10f;
    [SerializeField] private float spritAcceleration = 5f;
    [SerializeField] private float turningSpeed = 2f;
    [SerializeField] private float gravity = 9.81f;
    [SerializeField] private float jumpHeight = 0.2f;
    [SerializeField] float health = 100;
    [SerializeField] float healthMax = 100;



    private float verticalVelovity;
    private float speed;
    public bool playerDead;
    public bool isgrouded;

    [Header("Input")]
    private float moveInput;
    private float turnInput;

    [Header("----- Player Belt -----")]
    int beltAmount;
    [SerializeField] GameObject belt;
    public GameObject[] playerBelt;
    public int currentBeltSelection;

    private void Start()
    {
        //Get slots and add them to an array, set player belt slot ID to 200 - 203
        beltAmount = belt.transform.childCount;
        playerBelt = new GameObject[beltAmount];
        for (int i = 0; i < beltAmount; i++)
        {
            playerBelt[i] = belt.transform.GetChild(i).GetChild(0).gameObject;
            playerBelt[i].GetComponentInParent<SlotBackground>().SlotID = i + 200;
        }
        //Set First Slot of belt as Selected
        gameManager.instance.selectedSlot = playerBelt[0];
        gameManager.instance.previuslySelectedSlot = playerBelt[0];
        controller = GetComponent<CharacterController>();
        //Open and close inventory to populate
        gameManager.instance.ToggleInventory();
        gameManager.instance.UnPauseGame();
        //Enable visuals of inventory of selcted slot
        gameManager.instance.selectedSlot.GetComponentInParent<SlotBackground>().selected = true;
        gameManager.instance.selectedSlot.GetComponentInParent<SlotBackground>().UpdateSelection();
        //Player Health
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
            //Vector3 currentLookDirection = controller.velocity.normalized;
            //currentLookDirection.y = 0;
            //currentLookDirection.Normalize();

            //Quaternion targetRotation = Quaternion.LookRotation(currentLookDirection);

            //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * turningSpeed);

            // Calculate the camera's forward direction on the XZ plane
            Vector3 cameraForward = cameraThird.forward; 
            cameraForward.y = 0; 
            // Ignore the Y component
            cameraForward.Normalize(); // Calculate the current movement direction relative to the camera
            Vector3 moveDirection = new Vector3(turnInput, 0, moveInput).normalized; 
            Vector3 currentLookDirection = cameraThird.TransformDirection(moveDirection); currentLookDirection.y = 0; // Ignore the Y component
            currentLookDirection.Normalize(); // Calculate the desired rotation towards the current look direction
            Quaternion targetRotation = Quaternion.LookRotation(currentLookDirection); 
            // Apply the desired rotation to the character
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * turningSpeed);
        }
    }
    private float VerticalForceCalculation()
    {
        isgrouded = controller.isGrounded;
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
        if (Input.GetKeyDown(KeyCode.I))
        {
            gameManager.instance.ToggleInventory();
        }
        moveInput = Input.GetAxis("Vertical");
        turnInput = Input.GetAxis("Horizontal");
    }
    public void TakeDamage(float damage)
    {
        health -= damage;
        Debug.Log("you took " + damage + " Now health is " + health);
        UpdatePlayerHealth();
    }

    public void UpdatePlayerHealth()
    {
        gameManager.instance.playerHealthBar.fillAmount = health / healthMax;
    }
    public bool PlayerBeltHaveSpace(Item stats)
    {
        for (int i = 0; i < beltAmount; i++)
        {
            //Check if the item can be fit or not
            if (playerBelt[i].GetComponentInChildren<Slot>().GetID() == 0 || playerBelt[i].GetComponentInChildren<Slot>().GetID() == stats.ID && playerBelt[i].GetComponent<Slot>().GetItemStackAmount() < stats.stackMax)
            {
                return true;
            }
        }
        return false;
    }

    public bool AddItem(Item stats)
    {
        for (int i = 0; i < beltAmount; i++)
        {
            if (playerBelt[i].GetComponent<Slot>().GetItemStackAmount() == 0)
            {
                playerBelt[i].GetComponent<Slot>().IncrementStackBy(1);
                playerBelt[i].GetComponent<Slot>().AddItemToSlot(stats.ID, stats.type, stats.itemName, stats.description, stats.stackMax, stats.icon, stats.itemPrefab, stats.amountToAdd, stats.usable);
                playerBelt[i].GetComponent<Slot>().UpdateSlot();
                gameManager.instance.inventoryAud.PlayOneShot(gameManager.instance.pickup);
                return true;
            }
            else if (playerBelt[i].transform.GetComponent<Slot>().GetID() == stats.ID && playerBelt[i].GetComponent<Slot>().GetItemStackAmount() < stats.stackMax)
            {
                //Possible Error if item are not added correctly check here ----------< >
                playerBelt[i].GetComponent<Slot>().IncrementStackBy(1);
                playerBelt[i].GetComponent<Slot>().UpdateSlot();
                gameManager.instance.inventoryAud.PlayOneShot(gameManager.instance.pickup);
                return true;
            }
            gameManager.instance.selectedSlot.GetComponentInParent<SlotBackground>().UpdateSelection();
        }
        return false;
    }
}
