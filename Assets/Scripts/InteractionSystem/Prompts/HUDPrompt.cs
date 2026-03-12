using UnityEngine;
using TMPro;

public class HUDPrompt : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI promptText;

    //  Awake   //
    /*
        Hide Prompt
    */
    void Awake()
    {
        HideHUDPrompt();
    }

    //  Display HUD Prompt   //
    /*
        Check if prompt exists
        Update prompt
        Display prompt
    */
    public void DisplayHUDPrompt(string actionKey, string action)
    {
        if (promptText != null)
        {
            promptText.text = $"Press [{actionKey}] to {action}";
            gameObject.SetActive(true);
        }
    }

    //  Hide HUD Prompt   //
    /*
        Hide prompt
    */
    public void HideHUDPrompt()
    {
        gameObject.SetActive(false);
    }
}