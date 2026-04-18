using UnityEngine;

public class WorkBenchInteraction : MonoBehaviour, IInteractable
{
    //  Inspector Settings  //
    [SerializeField] private string promptText = "Work at Table";
    [SerializeField] private CameraController cameraController;

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
        cameraController.EnterWorkbench();
        Debug.Log($"[WorkBenchInteraction] Working at Table");
    }
}