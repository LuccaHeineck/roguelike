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
        slime.Animator.SetBool("isWalking", false);

        slime.agent.speed = slime.chaseMoveSpeed;
        slime.agent.SetDestination(slime.player.position);
    }

    public void Update()
    {
        if (slime.agent.velocity.magnitude > 0.1f)
        {
            Vector2 direction = slime.agent.velocity.normalized;

            slime.Animator.SetFloat("moveDirectionX", direction.x);
            slime.Animator.SetFloat("moveDirectionY", direction.y);
        }

        NavMeshPath path = new NavMeshPath();

        if (slime.CanSeePlayer())
            slime.agent.SetDestination(slime.player.position);
        else
            slime.StartIdle();
    }

    public void Exit() { }
}