using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private float movSpeed = 5f;

    public float MovSpeed => movSpeed;
    public Rigidbody2D Rb { get; private set; }
    public Animator Animator { get; private set; }
    public Vector2 MoveInput { get; private set; }
    public Vector2 LastMoveInput { get; private set; }
    public StateMachine StateMachine { get; private set; }
    public bool AttackPressed { get; private set; }
    public Health health;


    void Start()
    {
        Rb = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();

        StateMachine = new StateMachine();
        StateMachine.ChangeState(new PlayerIdleState(this));
    }

    void Update()
    {
        StateMachine.Update();
    }

    public void Move(InputAction.CallbackContext context)
    {
        MoveInput = context.ReadValue<Vector2>();

        if (MoveInput != Vector2.zero)
            LastMoveInput = MoveInput;
    }

    public void Attack(InputAction.CallbackContext context)
    {
        if (context.performed)
            AttackPressed = true;
    }

    public void ConsumeAttack() => AttackPressed = false;
}
