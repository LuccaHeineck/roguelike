using UnityEngine;

public class SlimeDeadState : IState
{
    private SlimeControl slime;

    public SlimeDeadState(SlimeControl slime) => this.slime = slime;

    public void Enter()
    {
        slime.Animator.SetBool("isDead", true);
        slime.Rb.linearVelocity = Vector2.zero;
    }

    public void Update() { }

    public void Exit()
    {
        slime.Animator.SetBool("isDead", false);
    }
}