using UnityEngine;

public class PlayerAttackState : IState
{
    private PlayerControl player;
    private float timer;
    private const float attackDuration = 0.54f;

    public PlayerAttackState(PlayerControl player) => this.player = player;

    public void Enter()
    {
        timer = 0f;
        player.Rb.linearVelocity = Vector2.zero;
        player.Animator.SetTrigger("Attacking");
    }

    public void Update()
    {
        timer += Time.deltaTime;

        if (timer >= attackDuration)
        {
            if (player.MoveInput != Vector2.zero)
                player.StateMachine.ChangeState(new PlayerRunningState(player));
            else
                player.StateMachine.ChangeState(new PlayerIdleState(player));
        }
    }

    public void Exit() => player.ConsumeAttack();
}