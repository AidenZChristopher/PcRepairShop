using UnityEngine;

public class WorkbenchSlot : MonoBehaviour
{
    public bool isOccupied = false;
    public GameObject occupyingItem = null;

    public void PlaceItem(GameObject item)
    {
        item.SetActive(true); // re-enable object in world
        item.transform.position = transform.position;
        item.transform.rotation = Quaternion.identity;
        occupyingItem = item;
        isOccupied = true;
    }

    public void ClearSlot()
    {
        occupyingItem = null;
        isOccupied = false;
    }
}