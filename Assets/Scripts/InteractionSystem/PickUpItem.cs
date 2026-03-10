using UnityEngine;

public class PickUpItem : MonoBehaviour, IInteractable
{
    //  Inspector Settings  //
    [SerializeField] private string promptText = "Pick Up";
    [SerializeField] private string actionKey = "E";

    //  IInteractable Properties    //
    public string GetPromptText => promptText;
    public string GetActionKey => actionKey;

    //  Interact    //
    /*

    */
    public void Interact()
    {
        Debug.Log($"[PickUpItem] Picked Up Item");
    }
}