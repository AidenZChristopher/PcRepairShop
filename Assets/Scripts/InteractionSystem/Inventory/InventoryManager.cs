using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    //  Public global variable that refrences this class. 
    //  This allows any script to refrence this class without adding it to the inspector window.
    public static InventoryManager Instance { get; private set; }
    [SerializeField] private int maxInventorySlots = 2;
    [SerializeField] private float dropOffset = 1f;
    private ItemData[] inventory;    // Array named inventory that stores ItemData
    private int activeSlotIndex = 0;
    private int secondarySlotIndex = 1;

    //  Awake   //
    /*
        Set up Instance / singleton
        Set up inventory array
    */
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        inventory = new ItemData[maxInventorySlots];
    }

    //  Update  //
    /*
        Checks if keys 1/2 are pressed and swaps the activeSlot accordingly
    */
    void Update()
    {
        if (UnityEngine.InputSystem.Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            SetActiveSlot(0);
            InventoryHUD.Instance.Refresh();
        }
        if (UnityEngine.InputSystem.Keyboard.current.digit2Key.wasPressedThisFrame)
        {
            SetActiveSlot(1);
            InventoryHUD.Instance.Refresh();
        }
    }

    //  SetActiveSlot   //
    /*
        Check if active slot is not equal to keypress index
            make active slot -> secondary slot
            secondary slot -> primary
    */
    void SetActiveSlot(int index)
    {
        if (activeSlotIndex != index)
        {
            secondarySlotIndex = activeSlotIndex;
            activeSlotIndex = index;
        }
    }

    //  AttemptAdd  //
    /*
        Check if active slot is empty 
            Add item to inventory
        Check if secondary slot is empty
            Add item to inventory
        Else debug inventory is full
    */
    public void AttemptAdd(ItemData item)
    {
        if (inventory[activeSlotIndex] == null) AddItem(item, activeSlotIndex);
        else if (inventory[secondarySlotIndex] == null) AddItem(item, secondarySlotIndex);
        else Debug.Log("[InventoryManager] Inventory is Full");
    }

    void AddItem(ItemData item, int inventoryIndex)
    {
        inventory[inventoryIndex] = item;
        item.gameObject.SetActive(false); //hide object in world
        Debug.Log($"[InventoryManager] Added {item.name} to slot {inventoryIndex}");
        InventoryHUD.Instance.Refresh();
    }

    public void AttemptDrop()
    {
        if (inventory[activeSlotIndex] == null) Debug.Log("[InventoryManager] Not holding an Item to drop");
        else DropItem(activeSlotIndex);
    }

    void DropItem(int inventoryIndex)
    {
        ItemData item = inventory[inventoryIndex];
        item.gameObject.SetActive(true);
        item.transform.position = new Vector3(
            transform.position.x + transform.forward.x * dropOffset, 
            item.GroundOffset, 
            transform.position.z + transform.forward.z * dropOffset
            );
        inventory[inventoryIndex] = null;
        Debug.Log($"[InventoryManager] Dropped {item.name} from slot {inventoryIndex}");
        InventoryHUD.Instance.Refresh();
    }

    //  Accessors for InventoryHUD  //
    public int GetActiveSlotIndex() => activeSlotIndex;
    public ItemData GetItemAt(int index) => inventory[index];

    
    //Returns the GameObject of the item in the active slot
 
    public GameObject GetEquippedItem()
    {
        ItemData item = inventory[activeSlotIndex];
        if (item == null) return null;
        return item.gameObject;
    }
    
    // Removes the active slot item from inventory after workbench drop
    public void RemoveEquippedItem()
    {
        inventory[activeSlotIndex] = null;
        InventoryHUD.Instance.Refresh();
        Debug.Log("[InventoryManager] Removed equipped item for workbench drop.");
    }

}

