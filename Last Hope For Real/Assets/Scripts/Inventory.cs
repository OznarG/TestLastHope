using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class Inventory : MonoBehaviour
{
    [SerializeField] GameObject inventoryHolder;
    GameObject[] slots;

    [Header("---Drag Stats---")]
    int slotAmount;
    int slotNumber;
    public TMP_Text itemDescription;
    public TMP_Text itemStats;
    public GameObject backButton;
    public bool isOpen;

    // Start is called before the first frame update
    void Awake()
    {
        //Get all the slot child of the slot holder and add them to a slot array
        slotAmount = inventoryHolder.transform.childCount;
        Debug.Log(slotAmount);
        slots = new GameObject[slotAmount];
        for (int i = 0; i < slotAmount; i++)
        {
            //Set slot on array to slot in the SlotHolder
            slots[i] = inventoryHolder.transform.GetChild(i).GetChild(0).gameObject;
            slots[i].GetComponentInParent<SlotBackground>().SlotID = i;
        }
    }

    public bool AddItem(Item stats)
    {

        for (int i = 0; i < slotAmount; i++)
        {
            //Add the passed Item to the next empty Slot
            if (slots[i].GetComponent<Slot>().GetItemStackMax() == 0)
            {
                slots[i].GetComponent<Slot>().IncrementStackBy(1);
                slots[i].GetComponent<Slot>().AddItemToSlot(stats.ID, stats.type, stats.itemName, stats.description, stats.stackMax, stats.icon, stats.itemPrefab, stats.amountToAdd, stats.usable);
                slots[i].GetComponent<Slot>().UpdateSlot();
                gameManager.instance.inventoryAud.PlayOneShot(gameManager.instance.pickup);
                return true;
            }
            //if the slot has the same item and it has space, add one //THIS IS ASSUMING ALL ITEMS WE PICK HAS ONE ONLY
            //We need to updated to take more than one 
            else if (slots[i].transform.GetComponent<Slot>().GetID() == stats.ID && slots[i].GetComponent<Slot>().GetItemStackAmount() < stats.stackMax)
            {
                slots[i].GetComponent<Slot>().IncrementStackBy(1);
                slots[i].GetComponent<Slot>().UpdateSlot();
                gameManager.instance.inventoryAud.PlayOneShot(gameManager.instance.pickup);
                return true;
            }
        }
        return false;
    }
}
