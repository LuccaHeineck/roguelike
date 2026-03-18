using Unity.VisualScripting;
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
    [HideInInspector] public NavMeshAgent agent;
    [HideInInspector] public int currentPointIndex;
    [SerializeField] private float detectionRange = 6f;
    private Health health;

    private void Awake()
    {
        health = GetComponent<Health>();
        agent = GetComponent<NavMeshAgent>();
        Rb = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        BoxCollider = GetComponent<BoxCollider2D>();

        StateMachine = new StateMachine();
    }
    void Start()
    {
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        currentPointIndex = 0;

        StateMachine.ChangeState(new SlimeIdleState(this));
    }


    void Update()
    {
        StateMachine.Update();
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
        if (agent.CalculatePath(player.position, path))
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
        health.OnDeath += Die;
    }

    private void OnDisable()
    {
        health.OnDeath -= Die;
    }

    public void Die()
    {
        StateMachine.ChangeState(new SlimeDeadState(this));
    }
}