/*============================================================
 * SnapPoint.cs
 * Attach to a child sphere GameObject on each draggable item.
 * Set IsTrigger on the SphereCollider and assign the part type.
 *============================================================*/
using UnityEngine;

public class SnapPoint : MonoBehaviour
{
    [SerializeField] private PartType partType;         //item's type
    [SerializeField] private PartType compatibleType;   // the type it snaps to
    [SerializeField] private Vector3 snapOffset = Vector3.zero;
    public Vector3 SnapOffset => snapOffset;
    private SnapPoint currentPartner = null;
    private bool isTouching = false;

    public bool IsTouching => isTouching;
    public SnapPoint CurrentPartner => currentPartner;
    public PartType PartType => partType;
    public PartType CompatibleType => compatibleType;

    /*------------------------------------------------------------
     * Trigger callbacks -- track overlap with compatible partner
     *------------------------------------------------------------*/
    void OnTriggerEnter(Collider other)
    {
        if (isTouching) return;

        SnapPoint other_snapPoint = other.GetComponent<SnapPoint>();
        if (other_snapPoint == null) return;
        if (other_snapPoint.PartType != compatibleType) return;

        currentPartner = other_snapPoint;
        isTouching = true;
        Debug.Log($"[SnapPoint] {gameObject.name} touching compatible part {other.gameObject.name}");
    }

    void OnTriggerExit(Collider other)
    {
        SnapPoint other_snapPoint = other.GetComponent<SnapPoint>();
        if (other_snapPoint == null) return;
        if (other_snapPoint.PartType != compatibleType) return;

        currentPartner = null;
        isTouching = false;
        Debug.Log($"[SnapPoint] {gameObject.name} separated from {other.gameObject.name}");
    }
    void OnDrawGizmos()
    {
        BoxCollider box = GetComponent<BoxCollider>();
        if (box == null) return;

        Gizmos.color = Color.red;
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawWireCube(box.center, box.size);
    }
}