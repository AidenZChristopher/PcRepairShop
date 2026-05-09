using UnityEngine;

public class TrashZone : MonoBehaviour
{
    private bool isOverlapping = false;
    private DraggablePart overlappingPart = null;

    public bool IsOverlapping => isOverlapping;
    void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent<DraggablePart>(out var part)) return;

        isOverlapping = true;
        overlappingPart = part;
        Debug.Log($"[TrashZone] {other.gameObject.name} entered trash zone");
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.TryGetComponent<DraggablePart>(out var part)) return;
        if (part != overlappingPart) return;

        isOverlapping = false;
        overlappingPart = null;
        Debug.Log($"[TrashZone] {other.gameObject.name} left trash zone");
    }

    public void TrashItem(GameObject item)
    {
        Debug.Log($"[TrashZone] Trashed {item.name}");
        Destroy(item);
        isOverlapping = false;
        overlappingPart = null;
    }
}