using UnityEngine;

public class PlayerIdleState : IState
{
    private PlayerControl player;

    public PlayerIdleState(PlayerControl player) => this.player = player;

    public void Enter()
    {
        player.Rb.linearVelocity = Vector2.zero;
        player.Animator.SetBool("isRunning", false);
        player.Animator.SetFloat("lastInputX", player.LastMoveInput.x);
        player.Animator.SetFloat("lastInputY", player.LastMoveInput.y);
    }

    public void Update()
    {
        if (player.AttackPressed)
        {
            player.ConsumeAttack();
            player.StateMachine.ChangeState(new PlayerAttackState(player));
            return;
        }

        if (player.MoveInput != Vector2.zero)
            player.StateMachine.ChangeState(new PlayerRunningState(player));
    }

    public void Exit() { }
}