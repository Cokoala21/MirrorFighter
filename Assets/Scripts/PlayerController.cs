using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController characterController;

    [SerializeField] 
    private float speed = 5f;
    
    [SerializeField]
    private float rotationSpeed = 720f; // Degrees per second

    private Animator animator;

    private float kickCooldown = 5f; // Cooldown duration
    private float lastKickTime = -3f; // To allow immediate kick at start

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Get input
        float verticalInput = Input.GetAxis("Vertical"); // Forward/Backward
        Vector3 moveDirection = transform.forward * verticalInput; // Move forward/backward

        // Move the character
        characterController.Move(moveDirection * Time.deltaTime * speed);

        // Check if moving
        bool isMoving = verticalInput != 0;
        animator.SetBool("IsWalking", isMoving);

        // Handle rotation (use mouse or right stick)
        float horizontalInput = Input.GetAxis("Horizontal"); // Left/Right
        if (horizontalInput != 0)
        {
            Quaternion turnRotation = Quaternion.Euler(0f, horizontalInput * rotationSpeed * Time.deltaTime, 0f);
            characterController.transform.rotation *= turnRotation; // Rotate tank
        }

        // Handle kick input with cooldown
        if (Input.GetKeyDown(KeyCode.L) && Time.time - lastKickTime >= kickCooldown)
        {
            animator.SetInteger("Kick", 1);
            lastKickTime = Time.time; 
        }
        else if (Input.GetKeyUp(KeyCode.L))
        {
            animator.SetInteger("Kick", 0);
        }

         if (Input.GetKeyDown(KeyCode.K) )
        {
            animator.SetInteger("Kick", 3);
            lastKickTime = Time.time; 
        }
        else if (Input.GetKeyUp(KeyCode.K))
        {
            animator.SetInteger("Kick", 2);
        }
    }
}
