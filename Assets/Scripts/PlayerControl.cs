using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    public float movSpeed = 5f;
    public Rigidbody2D rb;
    public InputAction playerControls;
    Vector2 moveDirection = Vector2.zero;

    void OnEnable()
    {
        playerControls.Enable();
    }

    void OnDisable()
    {
        playerControls.Disable();
    }

    void Update()
    {
        moveDirection = playerControls.ReadValue<Vector2>();
    }

    void FixedUpdate()
    {
        rb.linearVelocity = moveDirection * movSpeed;
    }
}
