using UnityEngine;

public class PlayerRunningState : IState
{
    private PlayerControl player;

    public PlayerRunningState(PlayerControl player) => this.player = player;

    public void Enter()
    {
        player.Animator.SetBool("isRunning", true);
    }

    public void Update()
    {
        player.Rb.linearVelocity = player.MoveInput * player.MovSpeed;
        player.Animator.SetFloat("InputX", player.MoveInput.x);
        player.Animator.SetFloat("InputY", player.MoveInput.y);

        if (player.MoveInput == Vector2.zero)
            player.StateMachine.ChangeState(new PlayerIdleState(player));
    }

    public void Exit() { }
}