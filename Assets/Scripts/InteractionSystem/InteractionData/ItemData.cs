using UnityEngine;


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
*/

public class ItemData : MonoBehaviour, IInteractable
{
    //  Inspector Settings  //
    [SerializeField] private string promptText = "Pick Up Case";

    //  IInteractable Properties    //
    public string GetPromptText => promptText;
    public void Interact()
    {
        Debug.Log($"[ItemData] Picked Up Item");
        InventoryManager.Instance.AttemptAdd(this);
    }
}