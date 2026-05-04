/*============================================================
 * DraggablePart.cs
 * Handles click-and-drag for parts sitting on the workbench.
 * Attach to each item GameObject that can be dragged.
 *============================================================*/
using UnityEngine;

public class DraggablePart : MonoBehaviour
{
    [SerializeField] private LayerMask tableLayer;

    private bool isDragging = false;
    private Camera workbenchCam;
    private WorkbenchSlot currentSlot;

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

        // release -- drop
        if (isDragging && UnityEngine.InputSystem.Mouse.current.leftButton.wasReleasedThisFrame)
            StopDrag();
    }

    /*------------------------------------------------------------
     * StartDrag
     *------------------------------------------------------------*/
    void StartDrag()
    {
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
     * StopDrag
     *------------------------------------------------------------*/
    void StopDrag()
    {
        isDragging = false;
        Debug.Log($"[DraggablePart] Dropped {gameObject.name}");
    }
}