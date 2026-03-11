using UnityEngine;

//  Inventory System    //
/*
    create Array called Inventory with max size of 2
    If 1 is pressed have InventorySlot1 become Active Slot - Do the same if 2 is pressed
    When "E" is pressed:
        Check if DetectionPoint is not null
            if detection point is not empty
                Check if Active Slot is null
                    Add Item to active slot
                If Active slot != null
                    Check if secondarySlot is empty
                    If secondarySlot is empty
                        Add Item
                    if secondarySlot is full
                    Give message saying inventory is full
            if detection point is empty
                Check if Active Slot is null
                    if Active slot is null do nothing
                    If Active slot != null
                        Drop Item
*/

//  InventoryHUD    //
/*
    Awake:
    Set Inventory settings
    Display Inventroy
    Highlight Active Inventory Slot

    Update:
    CheckActiveInventorySlot
    UpdateActiveInventorySlot - Highlight Active Inventory Slot 
*/

//  Item Data   //
/*
    Store Item Data: 
        Name, 
        Sprite Icon, 
        GameObject,
        promptText,
        actionKey,
    
    Pick Up:
        Debug Log
        Delete GameObject

    Set Down:
        Debug Log
        Place Object

*/

public class ItemData : MonoBehaviour, IInteractable
{
    //  Inspector Settings  //
    [SerializeField] private string promptText = "Pick Up";
    [SerializeField] private string actionKey = "E";

    //  IInteractable Properties    //
    public string GetPromptText => promptText;
    public string GetActionKey => actionKey;
    public void Interact()
    {
        Debug.Log($"[ItemData] Picked Up Item");
    }
}