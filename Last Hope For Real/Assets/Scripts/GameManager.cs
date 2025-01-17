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

    [Header("----- Inventory Management -----")]
    public GameObject previuslySelectedSlot = null;
    public GameObject selectedSlot = null;
    public Color backgroundColor;
    public AudioSource inventoryAud;
    public AudioClip pickup;

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

        Time.timeScale = 1;
        isPaused = false;
        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;
        activeMenu.SetActive(false);
        activeMenu = null;
    }
}
