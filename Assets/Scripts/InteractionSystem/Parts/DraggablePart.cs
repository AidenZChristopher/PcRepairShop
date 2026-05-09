//Claude AI was used to debug this code. All debugging suggestions were understood and implemented by the developer.
/*============================================================
 * DraggablePart.cs
 * Handles click-and-drag for parts sitting on the workbench.
 *============================================================*/
using UnityEngine;

public class DraggablePart : MonoBehaviour
{
    [SerializeField] private LayerMask tableLayer;

    private bool isDragging = false;
    private bool isSnapped = false;
    private Camera workbenchCam;
    private WorkbenchSlot currentSlot;
    private SnapPoint snapPoint;
    private Transform previousParent = null;
    private Vector3 originalScale; 
    private ItemData itemData;
    private TrashZone trashZone = null;

    public void SetTrashZone(TrashZone zone) => trashZone = zone;

    void Awake()
    {
        snapPoint = GetComponentInChildren<SnapPoint>();
        previousParent = transform.parent;
        originalScale = transform.localScale;
        itemData = GetComponent<ItemData>();

    }

    public void SetWorkbenchCam(Camera cam) => workbenchCam = cam;
    public void SetCurrentSlot(WorkbenchSlot slot) => currentSlot = slot;

    /*------------------------------------------------------------
     * Update -- handle drag input
     *------------------------------------------------------------*/
    void Update()
    {
        if (workbenchCam == null) return;

        // left click -- start drag if clicking this object
        if (UnityEngine.InputSystem.Mouse.current.leftButton.wasPressedThisFrame)
        {
            Ray ray = workbenchCam.ScreenPointToRay(
                UnityEngine.InputSystem.Mouse.current.position.ReadValue()
            );

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Debug.Log($"[DraggablePart] Raycast hit: {hit.collider.gameObject.name} | Expected: {gameObject.name}");
                if (hit.collider.gameObject == gameObject)
                    StartDrag();
            }
        }

        // hold -- move with cursor
        if (isDragging && UnityEngine.InputSystem.Mouse.current.leftButton.isPressed)
            DragToMouse();

        // release -- check for snap then drop
        if (isDragging && UnityEngine.InputSystem.Mouse.current.leftButton.wasReleasedThisFrame)
            StopDrag();

        // right click -- unsnap
        if (isSnapped && UnityEngine.InputSystem.Mouse.current.rightButton.wasPressedThisFrame)
        {
            Ray ray = workbenchCam.ScreenPointToRay(
                UnityEngine.InputSystem.Mouse.current.position.ReadValue()
            );

            if (Physics.Raycast(ray, out RaycastHit hit) && hit.collider.gameObject == gameObject)
                Unsnap();
        }
    }

    /*------------------------------------------------------------
     * StartDrag -- blocks drag if snapped
     *------------------------------------------------------------*/
    void StartDrag()
    {
        if (isSnapped) return;

        isDragging = true;

        if (currentSlot != null)
        {
            currentSlot.ClearSlot();
            currentSlot = null;
        }

        Debug.Log($"[DraggablePart] Started dragging {gameObject.name}");
    }

    /*------------------------------------------------------------
     * DragToMouse -- moves part along table surface
     *------------------------------------------------------------*/
    void DragToMouse()
    {
        Ray ray = workbenchCam.ScreenPointToRay(
            UnityEngine.InputSystem.Mouse.current.position.ReadValue()
        );

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, tableLayer))
        {
            Vector3 offset = itemData != null
                ? itemData.PlacementPositionOffset
                : new Vector3(0f, 0.05f, 0f);

            transform.position = new Vector3(
                hit.point.x + offset.x,
                hit.point.y + offset.y,
                hit.point.z + offset.z
            );
        }
    }

    /*------------------------------------------------------------
     * StopDrag -- snaps if SnapPoint is touching compatible partner
     *------------------------------------------------------------*/
void StopDrag()
{
    isDragging = false;

    if (trashZone != null && trashZone.IsOverlapping)
        {
            trashZone.TrashItem(gameObject);
            return;
        }
        
    if (snapPoint != null && snapPoint.IsTouching && snapPoint.CurrentPartner != null)
    {
        Transform partnerParent = snapPoint.CurrentPartner.transform.parent;
        if (partnerParent == null)
        {
            Debug.LogError($"[DraggablePart] Partner SnapPoint has no parent.");
            return;
        }

        Vector3 targetWorldPos = snapPoint.CurrentPartner.transform.position + snapPoint.SnapOffset;

        transform.SetParent(partnerParent, true);
        transform.position = targetWorldPos;
        transform.localRotation = Quaternion.identity;
        transform.localScale = originalScale;

        isSnapped = true;
        Debug.Log($"[DraggablePart] Snapped {gameObject.name} to {snapPoint.CurrentPartner.gameObject.name}");
    }
    else
    {
        // If not snapped finds the nearest workbench to snap to
        WorkBenchInteraction workbench = FindFirstObjectByType<WorkBenchInteraction>();
        WorkbenchSlot nearestSlot = workbench != null ? workbench.GetNearestOpenSlot(transform.position) : null;

        if (nearestSlot != null)
        {
            nearestSlot.PlaceItem(gameObject);
            Debug.Log($"[DraggablePart] Re-registered {gameObject.name} into slot {nearestSlot.gameObject.name}");
        }
        else
        {
            Debug.Log($"[DraggablePart] Dropped {gameObject.name} -- no open slot to register with");
        }
    }
}

    /*------------------------------------------------------------
     * Unsnap -- right click to release
     *------------------------------------------------------------*/
    void Unsnap()
    {
        transform.SetParent(previousParent);
        transform.localScale = originalScale; // reset scale on unsnap too
        isSnapped = false;
        Debug.Log($"[DraggablePart] Unsnapped {gameObject.name}");
    }
}