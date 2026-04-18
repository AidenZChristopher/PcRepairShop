using UnityEngine;

public class WorkBenchInteraction : MonoBehaviour, IInteractable
{
    //  Inspector Settings  //
    [SerializeField] private PlayerController playerController;
    [SerializeField] private string promptText = "Work at Table";
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

            Debug.Log($"[WorkBenchInteraction] Working at Table");
        }
        else
        {
            CameraManager.Instance.ExitWorkbench();
            workbenchInteraction = false;

            playerController.SetMovementEnabled(true); //allows playermovement
            playerController.SetVisibility(true); //Activates player model

            Debug.Log($"[WorkBenchInteraction] leaving Table");
        }
    }

    
}