using UnityEngine;
//Claude AI was used to generate part of this code. It was then edited by me to fit the needs of the project.
public class WorkbenchSlot : MonoBehaviour
{
    public bool isOccupied = false;
    public GameObject occupyingItem = null;

    public void PlaceItem(GameObject item)
    {
        item.SetActive(true);

        ItemData itemData = item.GetComponent<ItemData>();


        //Apply position offset from ItemData
        item.transform.position = transform.position
            + (itemData != null ? itemData.PlacementPositionOffset : Vector3.zero);

        // Apply rotation from ItemData
        item.transform.rotation = itemData != null ? itemData.PlacementRotation : Quaternion.identity;

        if (itemData != null) itemData.enabled = false;

        if (item.TryGetComponent<DraggablePart>(out var part))
        {
            part.SetCurrentSlot(this);
            var workbench = FindFirstObjectByType<WorkBenchInteraction>();
            if (workbench != null) 
            {
                part.SetWorkbenchCam(workbench.WorkbenchCamera);
                part.SetTrashZone(workbench.TrashZone);
            }

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