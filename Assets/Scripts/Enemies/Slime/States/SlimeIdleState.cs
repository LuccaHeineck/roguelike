using System.Threading;
using UnityEngine;

public class SlimeIdleState : IState
{
    private SlimeControl slime;
    private const float idleDuration = 5f;
    private float timer;

    public SlimeIdleState(SlimeControl slime) => this.slime = slime;

    public void Enter()
    {
        slime.Rb.linearVelocity = Vector2.zero;
        slime.Animator.SetBool("isWalking", false);
        slime.Animator.SetBool("isChasing", false);
        slime.Animator.SetBool("takingDamage", false);

        timer = 0;
    }

    public void Update()
    {
        timer += Time.deltaTime;

        if (timer >= idleDuration)
        {
            slime.Animator.SetBool("isWalking", true);
            slime.StateMachine.ChangeState(new SlimeWalkState(slime));
        }

    }

    public void Exit() { }
}