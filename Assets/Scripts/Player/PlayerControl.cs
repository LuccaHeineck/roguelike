using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class PlayerControl : MonoBehaviour
{
    private const float moveInputDeadZoneSqr = 0.0001f;

    [Header("Movement")]
    [SerializeField] private float dashDistance = 15f;
    [SerializeField] private float dashDuration = 0.15f;

    public float MovSpeed => Stats != null ? Stats.CurrentMoveSpeed : 0f;
    public float MovSpeedMultiplier { get; private set; }

    public float DashDistance => dashDistance;
    public float DashDuration => dashDuration;
    public Rigidbody2D Rb { get; private set; }
    public Animator Animator { get; private set; }
    public Vector2 MoveInput { get; private set; }
    public Vector2 LastMoveInput { get; private set; }
    public StateMachine StateMachine { get; private set; }
    public Health Health { get; private set; }
    public PlayerStats Stats { get; private set; }
    public bool AttackPressed { get; private set; }
    public bool DashPressed { get; private set; }
    public float LastDashAt { get; private set; }
    public float DashCooldown { get; private set; } = 0.5f;

    public PlayerIdleState IdleState { get; private set; }
    public PlayerRunState RunState { get; private set; }
    public PlayerAttackState AttackState { get; private set; }
    public PlayerDashState DashState { get; private set; }

    public bool HasMoveInput => MoveInput.sqrMagnitude > moveInputDeadZoneSqr;
    public Vector2 MoveDirection => HasMoveInput ? MoveInput.normalized : Vector2.zero;

    private bool attackRequested;

    void Awake()
    {
        StateMachine = new StateMachine();

        IdleState = new PlayerIdleState(this);
        RunState = new PlayerRunState(this);
        AttackState = new PlayerAttackState(this);
        DashState = new PlayerDashState(this);
    }

    void Start()
    {
        Rb = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        Stats = GetComponent<PlayerStats>();

        Rb.interpolation = RigidbodyInterpolation2D.Interpolate;

        StateMachine.ChangeState(IdleState);
    }

    void Update()
    {
        ResolveAttackRequest();
        UpdateAnimationSpeed();
        StateMachine.Update();
    }

    public void Move(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();

        MoveInput = input.sqrMagnitude > moveInputDeadZoneSqr ? input : Vector2.zero;

        if (MoveInput != Vector2.zero)
            LastMoveInput = MoveInput.normalized;
    }

    private void UpdateAnimationSpeed()
    {
        if (Stats != null && Stats.BaseMoveSpeed > 0.0f)
        {
            MovSpeedMultiplier = MovSpeed / Stats.BaseMoveSpeed;
            Animator.SetFloat("moveSpeedMultiplier", MovSpeedMultiplier);
        }
    }

    public void Attack(InputAction.CallbackContext context)
    {
        if (context.performed)
            attackRequested = true;
    }

    public void Dash(InputAction.CallbackContext context)
    {
        if (context.performed && (Time.time - LastDashAt > DashCooldown))
            DashPressed = true;
    }

    public void RegisterDashEnd() { LastDashAt = Time.time; }

    public void StartIdle()
    {
        StateMachine.ChangeState(IdleState);
    }

    public void StartRun()
    {
        StateMachine.ChangeState(RunState);
    }

    public void StartAttack()
    {
        ConsumeAttack();
        AttackState.ResetCombo();
        StateMachine.ChangeState(AttackState);
    }

    public void StartDash()
    {
        ConsumeDash();
        StateMachine.ChangeState(DashState);
    }

    public void ConsumeAttack() => AttackPressed = false;

    public void ConsumeDash() => DashPressed = false;

    private void ResolveAttackRequest()
    {
        if (!attackRequested)
        {
            return;
        }

        attackRequested = false;

        if (IsPointerOverUI())
        {
            return;
        }

        AttackPressed = true;
    }

    private static bool IsPointerOverUI()
    {
        if (EventSystem.current == null)
        {
            return false;
        }

        return EventSystem.current.IsPointerOverGameObject();
    }
}
