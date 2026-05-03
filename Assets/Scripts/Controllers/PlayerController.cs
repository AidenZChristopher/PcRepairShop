using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject playerMesh;

    private Rigidbody rb;

    private float movementX;
    private float movementY;

    //Animation
    private Animator animator;
    private bool isMoving = false;

    public float speed = 0;

    public bool rotateToFaceMovement = true;
    public float rotationSpeed = 10f;
    private bool movementLocked = false;

    void Awake()
    {
        animator = GetComponent<Animator>();
        if (animator == null) Debug.LogError("[PlayerController] No Animator found on Player!");
    }
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    void OnMove(InputValue movementValue)
    {

        if (movementLocked)
        {
            movementX = 0f;
            movementY = 0f;
            return;
        }

        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;

        Debug.Log($"[PlayerController] OnMove called, movementVector: {movementVector}, isMoving: {isMoving}");

        if (animator == null) return;

        if (isMoving == false && movementVector != Vector2.zero)
        {
            isMoving = true;
            Debug.Log("[PlayerController] Playing Walking animation");
            animator.SetBool("isMoving", true);
        }

        if (isMoving && movementVector == Vector2.zero)
        {
            isMoving = false;
            Debug.Log("[PlayerController] Playing Idle animation");
            animator.SetBool("isMoving", false);
        }
    }

    void FixedUpdate()
    {
        // Guard first
        if (movementLocked)
        {
            rb.linearVelocity = Vector3.zero;
            return;
        }

        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        rb.linearVelocity = new Vector3(
            movement.x * speed,
            rb.linearVelocity.y,
            movement.z * speed
        );

        if (rotateToFaceMovement && movement != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movement);
            rb.MoveRotation(Quaternion.Slerp(
                rb.rotation,
                targetRotation,
                rotationSpeed * Time.fixedDeltaTime
            ));
        }
    }
/*============================================================
 * Locks/unlocks player movement 
 *============================================================*/
public void SetMovementEnabled(bool enabled)
    {
        movementLocked = enabled == false; // true = locked

        // Clear velocity and input so player doesn't slide
        rb.linearVelocity = Vector3.zero;
        movementX = 0f;
        movementY = 0f;

        // Reset animation to idle cleanly
        isMoving = false;
        animator.SetBool("isMoving", false);
    }

/*============================================================
 * Toggles renderer only while keeping Animator and scripts active
 *============================================================*/
public void SetVisibility(bool visible)
    {
        foreach (var r in playerMesh.GetComponentsInChildren<SkinnedMeshRenderer>())
        r.enabled = visible;
    }
}