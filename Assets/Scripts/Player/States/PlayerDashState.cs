using System.Threading;
using UnityEngine;

public class PlayerDashState : IState
{
    private PlayerControl player;
    private float timer = 0f;
    private Vector2 initialDirection;

    public PlayerDashState(PlayerControl player) => this.player = player;

    public void Enter()
    {
        timer = 0f;
        initialDirection = player.LastMoveInput;

        player.Animator.SetBool("isRunning", true);
        player.Animator.SetFloat("lastInputX", player.LastMoveInput.x);
        player.Animator.SetFloat("lastInputY", player.LastMoveInput.y);
    }

    public void Update()
    {
        timer += Time.deltaTime;
        player.Rb.linearVelocity = initialDirection * (player.DashDistance / player.DashDuration);

        if (timer >= player.DashDuration)
        {
            timer = 0f;
            player.StartIdle();
            return;
        }

    }

    public void Exit()
    {
        player.RegisterDashEnd();
        player.Animator.SetBool("isRunning", false);
    }
}