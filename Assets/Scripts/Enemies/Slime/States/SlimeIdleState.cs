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
        slime.agent.speed = 0f;
        timer = 0;
    }

    public void Update()
    {
        if (slime.CloseToPlayer())
        {
            Debug.Log(" Esta perto do player! ");
            slime.StartAttack();
            return;
        }

        if (slime.CanSeePlayer())
        {
            slime.StartChase();
            return;
        }

        timer += Time.deltaTime;

        if (timer >= idleDuration)
        {
            slime.StartWalk();
            return;
        }

    }

    public void Exit()
    {
        slime.Animator.SetBool("isWalking", false);
        slime.Animator.SetBool("isChasing", false);
    }
}