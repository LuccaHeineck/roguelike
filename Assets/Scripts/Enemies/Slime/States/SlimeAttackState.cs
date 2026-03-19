using UnityEngine;

public class SlimeAttackState : IState
{
    private SlimeControl slime;

    private float timer;
    private const float attackDuration = 0.73f;

    public SlimeAttackState(SlimeControl slime)
    {
        this.slime = slime;
    }

    public void Enter()
    {
        slime.Animator.SetTrigger("Attacking");
        slime.isAttacking = true;

        timer = 0f;

        slime.agent.velocity = Vector3.zero;
        slime.Rb.linearVelocity = Vector2.zero;
        slime.agent.speed = 0f;
    }

    public void Update()
    {
        timer += Time.deltaTime;

        if (timer >= attackDuration)
        {
            slime.StartIdle();
        }
    }

    public void Exit()
    {
        slime.Animator.SetBool("isWalking", false);
        slime.Animator.SetBool("isChasing", false);
        slime.isAttacking = false;
    }

}