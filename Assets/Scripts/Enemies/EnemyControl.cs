using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    [SerializeField] private float walkMoveSpeed = 4f;
    [SerializeField] private float chaseMoveSpeed = 8f;
    public Rigidbody2D Rb { get; private set; }
    public Animator Animator { get; private set; }
    public StateMachine StateMachine { get; private set; }
    private Health health;

    void Start()
    {
        Rb = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();

        StateMachine = new StateMachine();
        //StateMachine.ChangeState(new SlimeIdleState(this));
    }

    private void Awake()
    {
        health = GetComponent<Health>();
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
        Destroy(gameObject);
    }
}