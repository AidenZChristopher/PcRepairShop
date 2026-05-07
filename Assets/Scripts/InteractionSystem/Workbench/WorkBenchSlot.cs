using UnityEngine;
//Claude AI was used to generate part of this code. It was then edited by me to fit the needs of the project.
public class WorkbenchSlot : MonoBehaviour
{
    public bool isOccupied = false;
    public GameObject occupyingItem = null;

public void PlaceItem(GameObject item)
{
    item.SetActive(true);
    item.transform.position = transform.position;

    ItemData itemData = item.GetComponent<ItemData>();
    item.transform.rotation = itemData != null ? itemData.PlacementRotation : Quaternion.identity;

    if (itemData != null) itemData.enabled = false;

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