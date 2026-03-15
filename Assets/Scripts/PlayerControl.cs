using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private float movSpeed = 5f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Vector2 moveInput;
    [SerializeField] private Animator animator;

    void Update()
    {
        rb.linearVelocity = moveInput * movSpeed;
    }

    public void Move(InputAction.CallbackContext context)
    {
        animator.SetBool("isRunning", true);

        if (context.canceled)
        {
            animator.SetBool("isRunning", false);
            animator.SetFloat("lastInputX", moveInput.x);
            animator.SetFloat("lastInputY", moveInput.y);
        }

        moveInput = context.ReadValue<Vector2>();
        animator.SetFloat("InputX", moveInput.x);
        animator.SetFloat("InputY", moveInput.y);
    }
}
