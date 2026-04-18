using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class CameraManager : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private Camera workbenchCamera;

    public static CameraManager Instance { get; private set; }

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        playerCamera.gameObject.SetActive(true);
        workbenchCamera.gameObject.SetActive(false);
    }

    public void EnterWorkbench()
    {
        playerCamera.gameObject.SetActive(false);
        workbenchCamera.gameObject.SetActive(true);
    }

    public void ExitWorkbench()
    {
        playerCamera.gameObject.SetActive(true);
        workbenchCamera.gameObject.SetActive(false);
    }
}