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
        slime.Animator.SetBool("isWalking", false);
        slime.Animator.SetBool("isChasing", false);

        slime.agent.speed = 0f;
        timer = 0;
    }

    public void Update()
    {
        if (slime.CanSeePlayer())
        {
            slime.Animator.SetBool("isChasing", true);
            slime.StartChase();
            return;
        }

        timer += Time.deltaTime;

        if (timer >= idleDuration)
        {
            slime.Animator.SetBool("isWalking", true);
            slime.StartWalk();
        }

    }

    public void Exit() { }
}