using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryHUD : MonoBehaviour
{
    //  Public global variable that references this class.
    public static InventoryHUD Instance { get; private set; }

    /* --- Inspector Settings --- */
    [Header("Slot Key Labels")]
    [SerializeField] private TextMeshProUGUI slot1KeyLabel;
    [SerializeField] private TextMeshProUGUI slot2KeyLabel;

    [Header("Slot Icons")]
    [SerializeField] private Image slot1Icon;
    [SerializeField] private Image slot2Icon;

    [Header("Slot Backgrounds")]
    [SerializeField] private Image slot1Background;
    [SerializeField] private Image slot2Background;

    [Header("Active Slot Settings")]
    [SerializeField] private Color activeColor   = new Color(1f, 1f, 1f, 1f);
    [SerializeField] private Color inactiveColor = new Color(0.4f, 0.4f, 0.4f, 1f);

    //  Awake   //
    /*
        Set up singleton
    */
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    //  Start   //
    /*
        Set key labels
        Draw initial state
    */
    void Start()
    {
        if (slot1KeyLabel != null) slot1KeyLabel.text = "1";
        if (slot2KeyLabel != null) slot2KeyLabel.text = "2";

        Refresh();
    }

    //  Refresh //
    /*
        Pull active slot from InventoryManager
        Update background tint for each slot
        Show icon if slot has an item, hide if empty
    */
    public void Refresh()
    {
        int activeSlot = InventoryManager.Instance.GetActiveSlotIndex();

        UpdateSlot(slot1Background, slot1Icon, 0, activeSlot);
        UpdateSlot(slot2Background, slot2Icon, 1, activeSlot);
    }

    //  UpdateSlot  //
    /*
        Tint background based on whether this slot is active
        Show icon if slot has an item, hide if empty
    */
    void UpdateSlot(Image background, Image icon, int slotIndex, int activeSlot)
    {
        bool isActive = slotIndex == activeSlot;

        if (background != null)
            background.color = isActive ? activeColor : inactiveColor;

        if (icon != null)
        {
            ItemData item = InventoryManager.Instance.GetItemAt(slotIndex);
            if (item != null && item.Icon != null)
            {
                icon.sprite  = item.Icon;
                icon.enabled = true;
            }
            else
            {
                icon.enabled = false;
            }
        }
    }
}