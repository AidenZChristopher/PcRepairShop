using UnityEngine;

public class ShelfInteraction : MonoBehaviour, IInteractable
{
    [SerializeField] private ItemData item;
    //Claude code made this ternary operator that declares GetPromptText and checks if an ItemData is assigned. If assigned return ItemData prompt otherwise use a default string
    public string GetPromptText => item != null ? item.GetPromptText : "Grab Item";

    public void Interact()
    {
        if (item == null)
        {
            Debug.LogWarning("[ShelfInteraction] No item assigned to shelf");
            return;
        }

        //create new object in world
        ItemData newItem = Instantiate(item);
        newItem.gameObject.SetActive(false);
        //add object to inventory
        InventoryManager.Instance.AttemptAdd(newItem);
    }
}