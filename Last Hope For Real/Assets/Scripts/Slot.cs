using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class Slot : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    //Slot information, it matches item information
    [Header("Item Information")]
    [SerializeField] int ID;
    [SerializeField] ItemType type;
    [SerializeField] string itemName;
    [SerializeField] string description;
    [SerializeField] int stackMax;
    [SerializeField] int stackAmount;
    [SerializeField] Sprite icon;
    [SerializeField] GameObject itemPrefab;
    [SerializeField] Sprite defaultImage;
    [SerializeField] int addAmount;
    [SerializeField] bool usable;

    [Header("Slot Information")]
    [SerializeField] bool selected;
    [SerializeField] Canvas canvas;
    bool isDragging;
    RectTransform rectTransform;
    Transform parentAfterDrag;

    
    private void Awake()
    {
        //Get transform of this item
        rectTransform = GetComponent<RectTransform>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        //Set parent to cursor
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        //Is not colliding with what is under
        transform.GetComponent<Image>().raycastTarget = false;
    }
    public void OnDrag(PointerEventData eventData)
    {
        // Get location of the mouse using canvas size and eventData
        isDragging = true;
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        //Remove cursor as parent and make the parent what it was before being grabed, is able to know what is bellow again
        isDragging = false;
        transform.SetParent(parentAfterDrag);
        transform.GetComponent<Image>().raycastTarget = true;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        // THIS IS CALLED ON THE ACTUAL BUTTON of the slot
    }

}
