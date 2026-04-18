using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class PlayerCamera : MonoBehaviour
{
    //  Takes offset value and keeps the camera at that locked distance.
    [SerializeField] private Transform player;
    private Vector3 offset;

    void Start()
    {
        offset = transform.position - player.position;
    }

    void LateUpdate()
    {
        transform.position = player.position + offset;
    }
}
