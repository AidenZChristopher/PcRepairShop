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

    void Awake()
    {
        snapPoint = GetComponentInChildren<SnapPoint>();
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
            transform.position = new Vector3(hit.point.x, hit.point.y + 0.05f, hit.point.z);
        }
    }

    /*------------------------------------------------------------
     * StopDrag -- snaps if SnapPoint is touching compatible partner
     *------------------------------------------------------------*/
    void StopDrag()
    {
        isDragging = false;

        if (snapPoint != null && snapPoint.IsTouching && snapPoint.CurrentPartner != null)
        {
            // snap to partner's parent item position
            transform.position = snapPoint.CurrentPartner.transform.parent.position;
            isSnapped = true;
            Debug.Log($"[DraggablePart] Snapped {gameObject.name}");
        }
        else
        {
            Debug.Log($"[DraggablePart] Dropped {gameObject.name} -- not touching compatible partner");
        }
    }

    /*------------------------------------------------------------
     * Unsnap -- right click to release
     *------------------------------------------------------------*/
    void Unsnap()
    {
        isSnapped = false;
        Debug.Log($"[DraggablePart] Unsnapped {gameObject.name}");
    }
}