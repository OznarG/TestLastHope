using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameManager : MonoBehaviour
{

    public static gameManager instance;

    [Header("---- Script References -----")]
    public PlayerController playerScript;
    public Inventory playerInventoryScript;

    [Header("---- UI references-----")]
    public Image playerHealthBar;

    [Header("----- Menus -----")]
    public bool isPaused;
    public GameObject activeMenu;
    public GameObject pauseMenu;
    public GameObject looseMenu;
    public GameObject playerInventory;
    public GameObject settingsMenu;
    public GameObject collectiblesMenu;
    public GameObject craftingMenu;
    public GameObject habilitiesMenu;


    [Header("----- Inventory Management -----")]
    public GameObject previuslySelectedSlot = null;
    public GameObject selectedSlot = null;
    public Color backgroundColor;
    public AudioSource inventoryAud;
    public AudioClip pickup;

    [Header("----- Inventory Management -----")]
    public GameObject raining;
    public GameObject snow;
    public bool isRaining;
    public bool isSnowing;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        playerInventoryScript = playerInventory.GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel") && !playerScript.playerDead)
        {
            isPaused = !isPaused;
            if (isPaused && activeMenu == null)
            {
                activeMenu = pauseMenu;                    
                activeMenu.gameObject.SetActive(isPaused);
                PauseGame();
            }
            else
            {
                UnPauseGame();

            }
        }
        //I HAVE TO FIX THIS ****************
        if(raining != null && snow != null)
            {
            if(isRaining)
            {
                raining.SetActive(isRaining);
            }
            if(isSnowing)
            {
                snow.SetActive(isSnowing);
            }
            if (!isRaining)
            {
                raining.SetActive(isRaining);
            }
            if (!isSnowing)
            {
                snow.SetActive(isSnowing);
            }
        }
       
    }
    public void PauseGame(bool cursorOn = true)
    {
        isPaused = true;
        Time.timeScale = 0;
        Cursor.visible = cursorOn;
        Cursor.lockState = CursorLockMode.Confined;
        
    }

    public void UnPauseGame()
    {
        if (activeMenu == playerInventoryScript.isOpen)
        {
            //Need replaced with belt
            selectedSlot.GetComponentInParent<SlotBackground>().selected = false;
            selectedSlot.GetComponentInParent<SlotBackground>().UpdateSelection();
            selectedSlot = previuslySelectedSlot;
            selectedSlot.GetComponentInParent<SlotBackground>().selected = true;
            selectedSlot.GetComponentInParent<SlotBackground>().UpdateSelection();

            playerInventoryScript.isOpen = false;
            instance.playerInventory.SetActive(false);
            //***IMPLEMENT WHEN I ADD WEAPON ****
            //if (selectedSlot.GetComponentInParent<SlotBackground>().GetComponentInChildren<Slot>().GetItemType() == 10)
            //{
            //    playerScript.currentWeapon.SetActive(true);
            //}
        }

        Time.timeScale = 1;
        isPaused = false;
        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;
        activeMenu.SetActive(false);
        activeMenu = null;
    }

    public void ToggleInventory()
    {
        if (!instance.isPaused)
        {
            //open Inventory and set as main
            instance.playerInventory.SetActive(true);
            instance.activeMenu = instance.playerInventory;
            instance.isPaused = true;
            instance.playerInventoryScript.isOpen = true;
            //NEED TO IMPLEMENT THIS IF WE ADD A BACK BUTTON ON THE MENU  ---------<===>
            //instance.playerinventoryScrpt.backButton.SetActive(false);
            previuslySelectedSlot = selectedSlot;
            //if(playerScript.currentWeapon != null)
            //{
            //  playerScript.currentWeapon.SetActive(false);
            //}           
            instance.PauseGame();
        }
        else if (instance.activeMenu == instance.playerInventory)
        {
            instance.UnPauseGame();
        }
    }
}
