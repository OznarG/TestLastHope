using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotBackground : MonoBehaviour, IDropHandler
{
    [SerializeField] private Slot child;
    public int SlotID;
    public bool selected;

    private void Awake()
    {
        selected = false;
        child = transform.GetComponentInChildren<Slot>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        //Get the slot component of the image that the Cursor is grabbing, then calculate how much free space to stack it has
        Slot sourceSlot = eventData.pointerDrag.GetComponent<Slot>();
        int freeSpace = child.GetFreeSpace();
        //If the item you grabing is equals to the item below it
        if (child.GetID() == sourceSlot.GetID())
        {
            //If it can hold all items that were grabbed
            if (freeSpace >= sourceSlot.GetItemStackAmount())
            {
                //Add all stock amount, delete item from source slot, Update the Slot(run checks etc), Update the source slote too
                child.IncrementStackBy(sourceSlot.GetItemStackAmount());
                sourceSlot.DecrementStackBy(sourceSlot.GetItemStackAmount());
                child.UpdateSlot();
                sourceSlot.UpdateSlot();
            }
            //If not all items fit
            else
            {
                //Fill it to max, subtract what you place on the child slot from the source slot, update both slots
                child.IncrementStackBy(freeSpace);
                sourceSlot.DecrementStackBy(freeSpace);
                child.UpdateSlot();
                sourceSlot.UpdateSlot();
            }
        }
        //If the items are not the same
        else
        {
            //Swap items location
            SwitchItemsLocation(sourceSlot);
        }
    }
    private void SwitchItemsLocation(Slot sourceSlot) /* NEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEE BACK HERE    */
    {
        // Create a temporary Slot to add the child information
        // (cannot call new on objects inheriting from MonoBehavior, so no copy constructor)
        int tempID = child.GetID();
        ItemType tempType = child.GetItemType();
        string tempItemName = child.GetItemName();
        string tempDescription = child.GetItemDescription();
        int tempStackMax = child.GetItemStackMax();
        int tempStackAmount = child.GetItemStackAmount();
        Sprite tempIcon = child.GetItemIcon();
        GameObject tempItemPrefab = child.GetItemPrefab();

        //Set this child information to the source slot
        child.SetItemID(sourceSlot.GetID());
        child.SetItemType(sourceSlot.GetItemType());
        child.SetItemName(sourceSlot.GetItemName());
        child.SetItemDescription(sourceSlot.GetItemDescription());
        child.SetItemStackMax(sourceSlot.GetItemStackMax());
        child.SetItemStackAmount(sourceSlot.GetItemStackAmount());
        child.SetItemIcon(sourceSlot.GetItemIcon());
        child.SetItemPrefab(sourceSlot.GetItemPrefab());

        //Set the source slot to the temporary slot from the child
        sourceSlot.SetItemID(tempID);
        sourceSlot.SetItemType(tempType);
        sourceSlot.SetItemName(tempItemName);
        sourceSlot.SetItemDescription(tempDescription);
        sourceSlot.SetItemStackMax(tempStackMax);
        sourceSlot.SetItemStackAmount(tempStackAmount);
        sourceSlot.SetItemIcon(tempIcon);
        sourceSlot.SetItemPrefab(tempItemPrefab);

        //Update both Slot
        child.UpdateSlot();
        sourceSlot.UpdateSlot();
    }
    public void UpdateSelection()
    {
        //if you check and select an item
        if (gameManager.instance.selectedSlot.GetComponentInParent<SlotBackground>().selected)
        {
            // THIS NEED IMPLEMENTATION TOO            -----------------------------------<=====================>
            //set this SlotBacground color to red
            transform.GetComponent<Image>().color = Color.red;
            //Add description into the description area
            //gameManager.instance.playerScript.itemDescription.text =
            //   "Descrition: \n" + child.GetItemDescription();

        }
        else
            //if is not selected do nothing, Set it white
            transform.GetComponent<Image>().color = gameManager.instance.backgroundColor;




    }
}
