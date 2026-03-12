using UnityEngine;

public class WorkBenchInteraction : MonoBehaviour, IInteractable
{
    //  Inspector Settings  //
    [SerializeField] private string promptText = "Work at Table";
    [SerializeField] private string actionKey = "F";

    //  IInteractable Properties    //
    public string GetPromptText => promptText;
    public string GetActionKey => actionKey;

    //  Interact    //
    /*

    */
    public void Interact()
    {
        Debug.Log($"[WorkBenchInteraction] Working at Table");
    }
}