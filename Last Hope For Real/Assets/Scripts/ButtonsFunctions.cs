using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsFunctions : MonoBehaviour
{
    public void Resume()
    {
        gameManager.instance.UnPauseGame();
    }
    
    public void BactToPauseMenu()
    {
        gameManager.instance.activeMenu.gameObject.SetActive(false);
        gameManager.instance.activeMenu = gameManager.instance.pauseMenu;
        gameManager.instance.activeMenu.gameObject.SetActive(true);
    }

    public void OpenInventory()
    {
        gameManager.instance.activeMenu.gameObject.SetActive(false);
        gameManager.instance.activeMenu = gameManager.instance.playerInventory;
        gameManager.instance.activeMenu.gameObject.SetActive(true);

        gameManager.instance.playerInventoryScript.isOpen = true;
        gameManager.instance.previuslySelectedSlot = gameManager.instance.selectedSlot;
    }

    public void CloseInventory()
    {
        gameManager.instance.selectedSlot.GetComponentInParent<SlotBackground>().selected = false;
        gameManager.instance.selectedSlot.GetComponentInParent<SlotBackground>().UpdateSelection();
        gameManager.instance.selectedSlot = gameManager.instance.previuslySelectedSlot;
        gameManager.instance.selectedSlot.GetComponentInParent<SlotBackground>().selected = true;
        gameManager.instance.selectedSlot.GetComponentInParent<SlotBackground>().UpdateSelection();

        gameManager.instance.playerInventoryScript.isOpen = false;
        gameManager.instance.playerInventory.SetActive(false);
        gameManager.instance.activeMenu.SetActive(false);
        gameManager.instance.activeMenu = gameManager.instance.pauseMenu;
        gameManager.instance.activeMenu.gameObject.SetActive(true);
    }
        public void OpenHabilitiesMenu()
    {
        gameManager.instance.activeMenu.gameObject.SetActive(false);
        gameManager.instance.activeMenu = gameManager.instance.habilitiesMenu;
        gameManager.instance.activeMenu.gameObject.SetActive(true);
    }

    public void OpenSettingsMenu()
    {
        gameManager.instance.activeMenu.gameObject.SetActive(false);
        gameManager.instance.activeMenu = gameManager.instance.settingsMenu;
        gameManager.instance.activeMenu.gameObject.SetActive(true);
    }

    public void OpenCollectibles()
    {
        gameManager.instance.activeMenu.gameObject.SetActive(false);
        gameManager.instance.activeMenu = gameManager.instance.collectiblesMenu;
        gameManager.instance.activeMenu.gameObject.SetActive(true);
    }

    public void OpenCraftMenu()
    {
        gameManager.instance.activeMenu.gameObject.SetActive(false);
        gameManager.instance.activeMenu = gameManager.instance.craftingMenu;
        gameManager.instance.activeMenu.gameObject.SetActive(true);
    }
}
