using UnityEngine;

public class PlayerRunState : IState
{
    private PlayerControl player;

    public PlayerRunState(PlayerControl player) => this.player = player;

    public void Enter()
    {
        player.Animator.SetBool("isRunning", true);
    }

    public void Update()
    {
        player.Rb.linearVelocity = player.MoveInput * player.MovSpeed;
        player.Animator.SetFloat("InputX", player.MoveInput.x);
        player.Animator.SetFloat("InputY", player.MoveInput.y);

        if (player.DashPressed)
        {
            player.StartDash();
            return;
        }

        if (player.AttackPressed)
        {
            player.StartAttack();
            return;
        }

        if (player.MoveInput == Vector2.zero)
            player.StartIdle();
    }

    public void Exit() { }
}