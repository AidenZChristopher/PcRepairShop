using UnityEngine;

public class WorkBenchInteraction : MonoBehaviour, IInteractable
{
    //  Inspector Settings  //
    [SerializeField] private string promptText = "Work at Table";

    //  IInteractable Properties    //
    public string GetPromptText => promptText;

    //  Interact    //
    /*

    */
    public void Interact()
    {
        Debug.Log($"[WorkBenchInteraction] Working at Table");
    }
}