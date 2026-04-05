using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Scripting.APIUpdating;

public class SlimeControl : MonoBehaviour
{
    public Transform player;
    public float walkMoveSpeed = 3f;
    public float chaseMoveSpeed = 7f;
    public Vector2 MoveDirection { get; private set; }
    public Rigidbody2D Rb { get; private set; }
    public Animator Animator { get; private set; }
    public StateMachine StateMachine { get; private set; }
    public BoxCollider2D BoxCollider { get; private set; }
    public Transform[] patrolPoints;
    public NavMeshAgent Agent { get; private set; }
    public CircleCollider2D attackHitbox;
    public float detectionRange = 6f;
    public float chasingRange = 10f;
    public Health Health { get; private set; }
    [HideInInspector] public bool IsAttacking { get; set; }
    [HideInInspector] public int CurrentPointIndex { get; set; }

    [Header ("Recompensa")]
    [SerializeField] private int xpRecompensa = 150;

    public SlimeIdleState IdleState { get; private set; }
    public SlimeWalkState WalkState { get; private set; }
    public SlimeChaseState ChaseState { get; private set; }
    public SlimeAttackState AttackState { get; private set; }
    public SlimeHurtState HurtState { get; private set; }
    public SlimeDeadState DeadState { get; private set; }

    private void Awake()
    {
        Health = GetComponent<Health>();
        Agent = GetComponent<NavMeshAgent>();
        Rb = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        BoxCollider = GetComponent<BoxCollider2D>();

        StateMachine = new StateMachine();

        IdleState = new SlimeIdleState(this);
        WalkState = new SlimeWalkState(this);
        ChaseState = new SlimeChaseState(this);
        AttackState = new SlimeAttackState(this);
        HurtState = new SlimeHurtState(this);
        DeadState = new SlimeDeadState(this);
    }
    void Start()
    {
        Agent.updateRotation = false;
        Agent.updateUpAxis = false;
        CurrentPointIndex = 0;

        StateMachine.ChangeState(IdleState);
    }

    void Update()
    {
        StateMachine.Update();
    }

    public void StartIdle()
    {
        StateMachine.ChangeState(IdleState);
    }

    public void StartWalk()
    {
        StateMachine.ChangeState(WalkState);
    }

    public void StartChase()
    {
        StateMachine.ChangeState(ChaseState);
    }

    public void StartAttack()
    {
        StateMachine.ChangeState(AttackState);
    }

    public void StartHurt()
    {
        if (!IsAttacking)
            StateMachine.ChangeState(HurtState);
    }

    private void EnableHitbox() => attackHitbox.enabled = true;
    private void DisableHitbox() => attackHitbox.enabled = false;

    public bool CloseToPlayer()
    {
        float distance = Vector2.Distance(player.transform.position, this.transform.position);

        if (distance <= 0.5 * attackHitbox.radius)
        {
            return true;
        }
        return false;
    }

    public bool CanSeePlayer()
    {
        NavMeshPath path = new NavMeshPath();

        if (DoesPathExist(path))
            if (PathLenght(path) != -1f)
                return true;
        return false;
    }

    public bool DoesPathExist(NavMeshPath path)
    {
        if (Agent.CalculatePath(player.position, path))
            if (path.status == NavMeshPathStatus.PathComplete)
                return true;
        return false;
    }

    public float PathLenght(NavMeshPath path)
    {
        float pathDistanceAcc = 0f;

        for (int i = 0; i < path.corners.Length - 1; i++)
        {
            pathDistanceAcc += Vector3.Distance(path.corners[i], path.corners[i + 1]);

            if (pathDistanceAcc > detectionRange)
                return -1f;
        }

        return pathDistanceAcc;
    }

    private void OnEnable()
    {
        Health.OnDeath += Die;
        Health.OnDamage += StartHurt;
    }

    private void OnDisable()
    {
        Health.OnDeath -= Die;
        Health.OnDamage -= StartHurt;
    }

    public void Die()
    {
        Experience playerExperience = player.GetComponent<Experience>();
        if (playerExperience != null)
            playerExperience.AddXP(xpRecompensa);
        StateMachine.ChangeState(DeadState);
    }

    public void DestroySlime() => Destroy(gameObject, 0.5f);
}