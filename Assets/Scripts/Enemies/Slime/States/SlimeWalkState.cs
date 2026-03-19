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

        slime.agent.speed = slime.walkMoveSpeed;
        slime.agent.SetDestination(slime.patrolPoints[slime.currentPointIndex].position);
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

        if (slime.agent.velocity.magnitude > 0.1f)
        {
            Vector2 direction = slime.agent.velocity.normalized;

            slime.Animator.SetFloat("moveDirectionX", direction.x);
            slime.Animator.SetFloat("moveDirectionY", direction.y);
        }

        if (!slime.agent.pathPending && slime.agent.remainingDistance < 0.1f)
        {
            if (slime.patrolPoints.Length > 0)
                slime.currentPointIndex = (slime.currentPointIndex + 1) % slime.patrolPoints.Length;
            else
                slime.currentPointIndex = 0;


            slime.StartIdle();
        }
    }

    public void Exit()
    {
        slime.Animator.SetBool("isWalking", false);
        slime.Animator.SetBool("isChasing", false);
    }
}