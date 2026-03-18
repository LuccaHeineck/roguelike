using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Scripting.APIUpdating;

public class SlimeControl : MonoBehaviour
{
    public float walkMoveSpeed = 3f;
    public float chaseMoveSpeed = 7f;
    public Vector2 MoveDirection { get; private set; }
    public Rigidbody2D Rb { get; private set; }
    public Animator Animator { get; private set; }
    public StateMachine StateMachine { get; private set; }
    public BoxCollider2D BoxCollider { get; private set; }
    [HideInInspector] public NavMeshAgent agent;
    [SerializeField] public Transform[] patrolPoints;
    [HideInInspector] public int currentPointIndex;
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


    private void OnEnable()
    {
        health.OnDeath += Die;
    }

    private void OnDisable()
    {
        health.OnDeath -= Die;
    }

    private void Die()
    {
        //StateMachine.CurrentState(new SlimeDeadState(this));
        Destroy(gameObject);
    }
}