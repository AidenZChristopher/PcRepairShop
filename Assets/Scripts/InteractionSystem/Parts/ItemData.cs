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
        promptText,
*/

public class ItemData : MonoBehaviour, IInteractable
{
    //  Inspector Settings  //
    [SerializeField] private string promptText;
    [SerializeField] private Sprite icon;
    [SerializeField] private float groundOffset = .25f; //default offset value
    [SerializeField] private Vector3 placementRotation = Vector3.zero; //rotate parts when placed on workbench
    [SerializeField] private Vector3 placementPositionOffset = Vector3.zero; //offset parts when placed on workbench
    public float GroundOffset => groundOffset;
    public Quaternion PlacementRotation => Quaternion.Euler(placementRotation);
    public Vector3 PlacementPositionOffset => placementPositionOffset;

    //  IInteractable Properties    //
    public string GetPromptText => promptText;
    public Sprite Icon => icon;
    public void Interact()
    {
        Debug.Log($"[ItemData] Picked Up Item");
        InventoryManager.Instance.AttemptAdd(this);
    }
}