using UnityEngine;

public class WorkbenchSlot : MonoBehaviour
{
    public bool isOccupied = false;
    public GameObject occupyingItem = null;

    public void PlaceItem(GameObject item)
    {
        item.SetActive(true);
        item.transform.position = transform.position;
        item.transform.rotation = Quaternion.identity;

        if (item.TryGetComponent<ItemData>(out var itemData))
            itemData.enabled = false;

        if (item.TryGetComponent<DraggablePart>(out var part))
        {
            part.SetCurrentSlot(this);
            var workbench = FindFirstObjectByType<WorkBenchInteraction>();
            if (workbench != null) part.SetWorkbenchCam(workbench.WorkbenchCamera);
        }

        occupyingItem = item;
        isOccupied = true;
    }

    public void ClearSlot()
    {
        occupyingItem = null;
        isOccupied = false;
    }
}