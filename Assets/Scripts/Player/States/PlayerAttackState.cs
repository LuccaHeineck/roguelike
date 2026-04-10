using UnityEngine;

public class PlayerAttackState : IState
{
    private PlayerControl player;
    private PlayerAttackHitboxController hitboxController;

    private float timer;
    private const float attackDuration = 0.54f;

    private int comboStep;
    private bool comboQueued;
    private const int maxComboStep = 2;

    public PlayerAttackState(PlayerControl player, int step = 0)
    {
        this.player = player;
        this.comboStep = step;
        hitboxController = player.GetComponent<PlayerAttackHitboxController>();
    }

    public void Enter()
    {
        comboQueued = false;
        timer = 0f;

        hitboxController.SetDirection(player.LastMoveInput);

        player.Animator.SetFloat("lastInputX", player.LastMoveInput.x);
        player.Animator.SetFloat("lastInputY", player.LastMoveInput.y);

        player.Animator.SetInteger("ComboStep", comboStep);
        player.Animator.SetTrigger("Attacking");
    }

    public void Update()
    {
        timer += Time.deltaTime;

        player.Rb.linearVelocity = player.Rb.linearVelocity * 0.99f;

        if (player.AttackPressed && !comboQueued)
        {
            comboQueued = true;
            player.ConsumeAttack();
        }

        if (timer >= attackDuration)
        {
            if (comboQueued && comboStep < maxComboStep - 1)
            {
                comboStep++;
                player.StateMachine.ChangeState(player.AttackState);
                return;
            }

            if (player.HasMoveInput)
                player.StartRun();
            else
                player.StartIdle();
        }
    }

    public void Exit() => player.ConsumeAttack();

    public void ResetCombo() => comboStep = 0;
}