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
    private bool isMoving;

    public float speed = 0;

    public bool rotateToFaceMovement = true;
    public float rotationSpeed = 10f;

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
    //Allows for freezing of playermovement. Called during workbench interaction
    public void SetMovementEnabled(bool enabled)
    {
        this.enabled = enabled;
        rb.linearVelocity = Vector3.zero; // clear any momentum on lock
        animator.SetBool("isMoving", false); // reset animation state
    }

    public void SetVisibility(bool visible) // Activates/deactives playerMesh
    {
        playerMesh.SetActive(visible);
    }
}