using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Item : ScriptableObject
{
    //this will take from scriptable object to be added into the inventory
    //IDs
    //-1 : SMG | -2 : Rifle | 5 : FirstAid | 50 : SMGAmmo | 51 : RifleAmmo | 100 : Upgrate Pieces
    public int ID;
    //Types 2: Heals the character | Type 10: Weapons | Type 5: Ammo | 
    public ItemType type;
    public string itemName;
    public string description;
    public int stackMax;
    public bool usable;
    public int amountToAdd;
    public Sprite icon;
    public GameObject itemPrefab;


}
