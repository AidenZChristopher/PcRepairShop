using UnityEngine;

public class WorkBenchInteraction : MonoBehaviour, IInteractable
{
    //  Inspector Settings  //
    [SerializeField] private string promptText = "Work at Table";
    [SerializeField] private CameraManager cameraManager;

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
        cameraManager.EnterWorkbench();
        Debug.Log($"[WorkBenchInteraction] Working at Table");
    }
}