using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private CharacterController characterController;

    [SerializeField] 
    private float speed = 5f;

    [SerializeField]
    private float rotationSpeed = 720f; // Degrees per second

    private Animator animator;

    private float kickCooldown = 3f; // Cooldown duration
    private bool isKicking = false;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        
        float verticalInput = Input.GetAxis("Vertical"); 
        if (verticalInput != 0)
        {
            Vector3 moveDirection = transform.forward * verticalInput; 
            characterController.Move(moveDirection * Time.deltaTime * speed);
        }

        
        bool isMoving = verticalInput != 0;
        animator.SetBool("IsWalking", isMoving);

      
        float horizontalInput = Input.GetAxis("Horizontal"); 
        if (horizontalInput != 0)
        {
            Quaternion turnRotation = Quaternion.Euler(0f, horizontalInput * rotationSpeed * Time.deltaTime, 0f);
            characterController.transform.rotation *= turnRotation; 
        }

        
        if (Input.GetKeyDown(KeyCode.L) && !isKicking)
        {
            StartCoroutine(KickCoroutine(1, 0)); // Normal kick
        }

        if (Input.GetKeyDown(KeyCode.K) && !isKicking)
        {
            StartCoroutine(KickCoroutine(3, 2)); // Special kick
        }
    }

    private IEnumerator KickCoroutine(int kickStartValue, int kickEndValue)
    {
        isKicking = true;
        animator.SetInteger("Kick", kickStartValue);
        yield return new WaitForSeconds(kickCooldown);
        animator.SetInteger("Kick", kickEndValue);
        isKicking = false;
    }
}
