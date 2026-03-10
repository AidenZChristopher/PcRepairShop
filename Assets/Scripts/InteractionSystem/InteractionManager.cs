using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionManager : MonoBehaviour
{
    [Header("Interaction Settings")]
    [SerializeField] private float interactRange = 0.4f;
    [SerializeField] private Vector3 detectionOffset = new Vector3(0f, 0f, 0.8f);
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private PlayerPrompt playerPrompt;
    [SerializeField] private HUDPrompt hudPrompt;

    private IInteractable currentInteractable;
    private InputAction interactAction;

    //  Start   //
    /*
        Create and enable interact input action
    */
    void Start()
    {
        interactAction = new InputAction(type: InputActionType.Button, binding: "<Keyboard>/e");
        interactAction.Enable();
    }

    //  On Destroy  //
    /*
        Disable and dispose interact input action
    */
    void OnDestroy()
    {
        interactAction.Disable();
        interactAction.Dispose();
    }

    //  Update  //
    /*
        Find closest interactable
        Check if action key is pressed
        Trigger interact
    */
    void Update()
    {
        FindClosestInteractable();

        if (currentInteractable == null) return;

        if (interactAction.WasPressedThisFrame())
            TriggerInteract();
    }

    //  Find Closest Interactable   //
    /*
        Get all colliders in range on Interactable layer
        Find the closest one with an IInteractable component
        Show or hide prompts based on result
    */
    void FindClosestInteractable()
    {
        Vector3 detectionPoint = transform.position + transform.TransformDirection(detectionOffset);
        Collider[] hits = Physics.OverlapSphere(detectionPoint, interactRange, interactableLayer);

        IInteractable closest = null;
        float closestDist = Mathf.Infinity;

        foreach (var hit in hits)
        {
            if (!hit.TryGetComponent<IInteractable>(out var interactable)) continue;

            float dist = Vector3.Distance(detectionPoint, hit.transform.position);
            if (dist < closestDist)
            {
                closestDist = dist;
                closest = interactable;
            }
        }

        if (closest != currentInteractable)
        {
            currentInteractable = closest;

            if (currentInteractable != null)
                ShowPrompts();
            else
                HidePrompts();
        }
    }

    //  Trigger Interact    //
    /*
        Call Interact on current interactable
        Clear interactable
    */
    void TriggerInteract()
    {
        currentInteractable.Interact();
        currentInteractable = null;
        HidePrompts();
    }

    //  Show Prompts    //
    /*
        Display PlayerPrompt with item text
        Display HUDPrompt with key and text
    */
    void ShowPrompts()
    {
        playerPrompt.DisplayPlayerPrompt(currentInteractable.GetPromptText);
        hudPrompt.DisplayHUDPrompt(currentInteractable.GetActionKey, currentInteractable.GetPromptText);
    }

    //  Hide Prompts    //
    /*
        Hide PlayerPrompt
        Hide HUDPrompt
    */
    void HidePrompts()
    {
        playerPrompt.HidePlayerPrompt();
        hudPrompt.HideHUDPrompt();
    }

    //  On Draw Gizmos  //
    /*
        Draw detection sphere in editor
    */
    void OnDrawGizmos()
    {
        Vector3 detectionPoint = transform.position + transform.TransformDirection(detectionOffset);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(detectionPoint, interactRange);
    }
}