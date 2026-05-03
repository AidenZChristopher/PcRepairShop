using UnityEngine;

public class WorkBenchInteraction : MonoBehaviour, IInteractable
{
    //  Inspector Settings  //
    [SerializeField] private PlayerController playerController;
    [SerializeField] private string promptText = "Work at Table";
    [SerializeField] private GameObject playerPromptCanvas;
    [SerializeField] private WorkbenchSlot[] slots; 

    private bool workbenchInteraction = false;


    //  IInteractable Properties    //
    public string GetPromptText => promptText;

    //  Interact    //
    /*
        Swap Camera to workbench Camera.
        Disable Player movement, and character model
        Open UI
    */
    public void Interact()
    {
        if (!workbenchInteraction)
        {
            CameraManager.Instance.EnterWorkbench();
            workbenchInteraction = true;

            playerController.SetMovementEnabled(false); //disable player movements
            playerController.SetVisibility(false);  //removes PlayerModel
            playerPromptCanvas.SetActive(false);

            Debug.Log($"[WorkBenchInteraction] Working at Table");
        }
        else
        {
            CameraManager.Instance.ExitWorkbench();
            workbenchInteraction = false;

            playerController.SetMovementEnabled(true); //allows playermovement
            playerController.SetVisibility(true); //Activates player model
            playerPromptCanvas.SetActive(true);

            Debug.Log($"[WorkBenchInteraction] leaving Table");
        }
    }

void Update()
{
    if (!workbenchInteraction) return;
    if (!UnityEngine.InputSystem.Keyboard.current.fKey.wasPressedThisFrame) return;

    TryDropEquippedItem();
}

    /// Pulls the equipped item from InventoryManager and places it at the first available slot on the table.
    private void TryDropEquippedItem()
    {
        GameObject equippedItem = InventoryManager.Instance.GetEquippedItem();

        if (equippedItem == null)
        {
            Debug.Log("[WorkBenchInteraction] No item equipped to drop.");
            return;
        }

        WorkbenchSlot availableSlot = GetFirstOpenSlot();

        if (availableSlot == null)
        {
            Debug.Log("[WorkBenchInteraction] No open slots on the table.");
            return;
        }

        availableSlot.PlaceItem(equippedItem);
        InventoryManager.Instance.RemoveEquippedItem();

        Debug.Log($"[WorkBenchInteraction] Dropped {equippedItem.name} onto table.");
    }

    //Gets the first open slot on the workbench, returns null if all slots are occupied
    private WorkbenchSlot GetFirstOpenSlot()
    {
        foreach (WorkbenchSlot slot in slots)
        {
            if (!slot.isOccupied) return slot;
        }
        return null;
    }
}