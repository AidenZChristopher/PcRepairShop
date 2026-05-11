using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class WorkbenchChecklistUI : MonoBehaviour
{
    public static WorkbenchChecklistUI Instance { get; private set; }

    [Header("UI References")]
    [SerializeField] private GameObject checklistPanel;
    [SerializeField] private Transform entryContainer;   // parent for entries
    [SerializeField] private GameObject entryPrefab;     // prefab with TMP label + checkmark image

    void Awake()
    {
        Instance = this;
        checklistPanel.SetActive(false);
    }
    public void ShowChecklist(PCCase pcCase)
    {
        foreach (Transform child in entryContainer)
            Destroy(child.gameObject);

        foreach (PartType part in pcCase.RequiredParts)
        {
            GameObject entry = Instantiate(entryPrefab, entryContainer);
            ChecklistEntry entryScript = entry.GetComponent<ChecklistEntry>();
            Debug.Log($"[WorkbenchChecklistUI] Spawning entry for {part} -- entryScript null: {entryScript == null}");
            entryScript.Setup(part, pcCase.IsPartInstalled(part));
        }

        checklistPanel.SetActive(true);
    }
    public void UpdateChecklist(PartType[] requiredParts, HashSet<PartType> installedParts)
    {
        ChecklistEntry[] entries = entryContainer.GetComponentsInChildren<ChecklistEntry>();

        foreach (ChecklistEntry entry in entries)
        {
            if (installedParts.Contains(entry.PartType))
                entry.SetChecked(true);
        }
    }

    public void HideChecklist()
    {
        checklistPanel.SetActive(false);
    }
}