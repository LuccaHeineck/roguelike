using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private float movSpeed = 5f;
    [SerializeField] private float dashDistance = 15f;
    [SerializeField] private float dashDuration = 0.15f;

    public float MovSpeed => movSpeed;
    public float DashDistance => dashDistance;
    public float DashDuration => dashDuration;
    public Rigidbody2D Rb { get; private set; }
    public Animator Animator { get; private set; }
    public Vector2 MoveInput { get; private set; }
    public Vector2 LastMoveInput { get; private set; }
    public StateMachine StateMachine { get; private set; }
    public Health Health { get; private set; }
    public bool AttackPressed { get; private set; }
    public bool DashPressed { get; private set; }
    public float LastDashAt { get; private set; }
    public float DashCooldown { get; private set; } = 0.5f;


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

    public void Dash(InputAction.CallbackContext context)
    {
        if (context.performed && (Time.time - LastDashAt > DashCooldown))
            DashPressed = true;
    }

    public void RegisterDashEnd() { LastDashAt = Time.time; }

    public void ConsumeAttack() => AttackPressed = false;

    public void ConsumeDash() => DashPressed = false;
}
