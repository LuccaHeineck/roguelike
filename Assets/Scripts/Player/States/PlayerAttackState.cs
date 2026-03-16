using UnityEngine;

public class PlayerAttackState : IState
{
    private PlayerControl player;
    private float timer;
    private const float attackDuration = 0.54f;

    private int comboStep;
    private bool comboQueued;
    private const int maxComboStep = 2;

    public PlayerAttackState(PlayerControl player, int step = 0)
    {
        this.player = player;
        this.comboStep = step;
    }

    public void Enter()
    {
        comboQueued = false;
        timer = 0f;
        player.Rb.linearVelocity = Vector2.zero;

        player.Animator.SetFloat("lastInputX", player.LastMoveInput.x);
        player.Animator.SetFloat("lastInputY", player.LastMoveInput.y);

        player.Animator.SetInteger("ComboStep", comboStep);
        player.Animator.SetTrigger("Attacking");
    }

    public void Update()
    {
        timer += Time.deltaTime;

        if (player.AttackPressed && !comboQueued)
        {
            comboQueued = true;
            player.ConsumeAttack();
        }

        if (timer >= attackDuration)
        {
            if (comboQueued && comboStep < maxComboStep - 1)
            {
                player.StateMachine.ChangeState(new PlayerAttackState(player, comboStep + 1));
                return;
            }

            if (player.MoveInput != Vector2.zero)
                player.StateMachine.ChangeState(new PlayerRunningState(player));
            else
                player.StateMachine.ChangeState(new PlayerIdleState(player));
        }
    }

    public void Exit() => player.ConsumeAttack();
}