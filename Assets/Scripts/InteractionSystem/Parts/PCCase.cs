using UnityEngine;
using System.Collections.Generic;

/*============================================================
 * PCCase.cs
 * Holds the required parts list for this case.
 * Notifies the checklist UI when a part is snapped in.
 *============================================================*/
public class PCCase : MonoBehaviour
{
    [Header("Required Parts")]
    [SerializeField] private PartType[] requiredParts;

    private HashSet<PartType> installedParts = new HashSet<PartType>();

    public PartType[] RequiredParts => requiredParts;
    public void NotifyPartInstalled(PartType partType)
    {
        if (!installedParts.Contains(partType))
        {
            installedParts.Add(partType);
            Debug.Log($"[PCCase] Installed: {partType}");

            WorkbenchChecklistUI.Instance?.UpdateChecklist(requiredParts, installedParts);
        }
    }
    public bool IsPartInstalled(PartType partType) => installedParts.Contains(partType);
}