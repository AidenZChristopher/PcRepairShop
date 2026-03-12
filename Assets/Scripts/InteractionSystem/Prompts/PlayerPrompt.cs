using UnityEngine;
using TMPro;

public class PlayerPrompt : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI promptText;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Vector3 offset = new Vector3(0f, 2.5f, 0f);

    private Camera mainCam;

    //  Awake   //
    /*
        Hide Prompt
    */
    void Awake()
    {
        HidePlayerPrompt();
    }

    //  Start   //
    /*
        Assign mainCamera
    */
    void Start()
    {
        mainCam = Camera.main;
    }

    //  Late Update //
    /*
        Follow player position with offset
        Billboard to face camera
    */
    void LateUpdate()
    {
        if (playerTransform != null)
            transform.position = playerTransform.position + offset;

        if (mainCam != null)
        {
            var rotation = mainCam.transform.rotation;
            transform.LookAt(transform.position + rotation * Vector3.forward, rotation * Vector3.up);
        }
    }

    //  Display Prompt   //
    /*
        Check if item has promptText
        Set prompt text as prompt
        Display prompt
    */
    public void DisplayPlayerPrompt(string prompt)
    {
        if (promptText != null)
        {
            promptText.text = prompt;
            gameObject.SetActive(true);
        }
    }

    //  Hide Player Prompt  //
    /*
        Hide prompt
    */
    public void HidePlayerPrompt()
    {
        gameObject.SetActive(false);
    }
}