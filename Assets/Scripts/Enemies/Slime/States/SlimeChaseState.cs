using System.Data;
using NavMeshPlus.Extensions;
using UnityEngine;
using UnityEngine.AI;

public class SlimeChaseState : IState
{
    private SlimeControl slime;

    public SlimeChaseState(SlimeControl slimeControl)
    {
        this.slime = slimeControl;
    }

    public void Enter()
    {
        slime.Animator.SetBool("isChasing", true);

        slime.Agent.speed = slime.chaseMoveSpeed;
        slime.Agent.SetDestination(slime.player.position);
    }

    public void Update()
    {
        if (slime.Agent.velocity.magnitude > 0.1f)
        {
            Vector2 direction = slime.Agent.velocity.normalized;

            slime.Animator.SetFloat("moveDirectionX", direction.x);
            slime.Animator.SetFloat("moveDirectionY", direction.y);
        }

        if (slime.CloseToPlayer())
        {
            slime.StartAttack();
            return;
        }

        NavMeshPath path = new NavMeshPath();

        if (slime.CanSeePlayer())
            slime.Agent.SetDestination(slime.player.position);
        else
            slime.StartIdle();
    }

    public void Exit()
    {
        slime.Animator.SetBool("isChasing", false);
    }
}