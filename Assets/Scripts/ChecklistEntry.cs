using UnityEngine;
using UnityEngine.UI;
using TMPro;

/*============================================================
 * ChecklistEntry.cs
 * One row in the checklist -- part name + checkmark.
 *============================================================*/
public class ChecklistEntry : MonoBehaviour
{
    [SerializeField] private TMP_Text label;
    [SerializeField] private GameObject checkmark;   // Image or tick GameObject

    public PartType PartType { get; private set; }

    public void Setup(PartType partType, bool isInstalled)
    {
        PartType = partType;
        Debug.Log($"[ChecklistEntry] label null: {label == null}");
        label.text = partType.ToString();
        checkmark.SetActive(isInstalled);
        Debug.Log($"[ChecklistEntry] Setup called -- {partType}");
    }

    public void SetChecked(bool isChecked)
    {
        checkmark.SetActive(isChecked);
    }
}