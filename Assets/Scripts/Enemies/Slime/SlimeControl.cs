using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class SlimeControl : MonoBehaviour
{
    [SerializeField] private float walkMoveSpeed = 4f;
    [SerializeField] private float chaseMoveSpeed = 8f;
    public Vector2 MoveDirection { get; private set; }
    public Vector2 LastMoveDirection { get; private set; }
    public Rigidbody2D Rb { get; private set; }
    public Animator Animator { get; private set; }
    public StateMachine StateMachine { get; private set; }
    public BoxCollider2D BoxCollider { get; private set; }
    private Health health;
    public bool colliding;

    void Start()
    {
        Rb = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        Animator.SetBool("isDead", false);
        BoxCollider = GetComponent<BoxCollider2D>();

        LastMoveDirection = new Vector2(0, -1);

        StateMachine = new StateMachine();
        StateMachine.ChangeState(new SlimeIdleState(this));
    }

    private void Awake()
    {
        health = GetComponent<Health>();
        colliding = false;
    }

    void Update()
    {
        StateMachine.Update();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("wall"))
            colliding = true;
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