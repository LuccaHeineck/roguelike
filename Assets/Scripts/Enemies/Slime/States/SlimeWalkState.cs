using System.Data;
using NavMeshPlus.Extensions;
using UnityEngine;
using UnityEngine.AI;

public class SlimeWalkState : IState
{
    private SlimeControl slime;

    public SlimeWalkState(SlimeControl slimeControl)
    {
        this.slime = slimeControl;
    }

    public void Enter()
    {
        slime.Animator.SetBool("isWalking", true);

        slime.Agent.speed = slime.walkMoveSpeed;
        slime.Agent.SetDestination(slime.patrolPoints[slime.CurrentPointIndex].position);
    }

    public void Update()
    {
        if (slime.CloseToPlayer())
        {
            slime.StartAttack();
            return;
        }

        if (slime.CanSeePlayer())
            slime.StartChase();

        if (slime.Agent.velocity.magnitude > 0.1f)
        {
            Vector2 direction = slime.Agent.velocity.normalized;

            slime.Animator.SetFloat("moveDirectionX", direction.x);
            slime.Animator.SetFloat("moveDirectionY", direction.y);
        }

        if (!slime.Agent.pathPending && slime.Agent.remainingDistance < 0.1f)
        {
            if (slime.patrolPoints.Length > 0)
                slime.CurrentPointIndex = (slime.CurrentPointIndex + 1) % slime.patrolPoints.Length;
            else
                slime.CurrentPointIndex = 0;


            slime.StartIdle();
        }
    }

    public void Exit()
    {
        slime.Animator.SetBool("isWalking", false);
    }
}