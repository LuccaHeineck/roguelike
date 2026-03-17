using UnityEngine;

public class SlimeIdleState : IState
{
    private SlimeControl slime;

    public SlimeIdleState(SlimeControl slime) => this.slime = slime;

    public void Enter()
    {
        slime.Rb.linearVelocity = Vector2.zero;
        slime.Animator.SetBool("isWalking", false);
        slime.Animator.SetBool("isChasing", false);
        slime.Animator.SetBool("takingDamage", false);

        slime.Animator.SetFloat("lastInputX", slime.MoveDirection.x);
        slime.Animator.SetFloat("lastInputY", slime.MoveDirection.y);
    }

    public void Update()
    {
    }

    public void Exit() { }
}