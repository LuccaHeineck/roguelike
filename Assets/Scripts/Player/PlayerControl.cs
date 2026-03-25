using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private float movSpeed = 5f;
    [SerializeField] private float dashDistance = 15f;
    [SerializeField] private float dashDuration = 0.15f;

    public float MovSpeed => movSpeed + (Stats != null ? Stats.MoveSpeedBonus : 0f);
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

        StateMachine.ChangeState(IdleState);
    }

    void Update()
    {
        ResolveAttackRequest();
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
