using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class SlimeHurtState : IState
{
    private SlimeControl slime;
    private const float hurtAnimationDuration = 0.33f;
    private float timer;

    public SlimeHurtState(SlimeControl slime) => this.slime = slime;

    public void Enter()
    {
        slime.Animator.SetBool("isWalking", false);
        slime.Animator.SetBool("isChasing", false);
        slime.Animator.SetTrigger("takingDamage");

        slime.agent.speed = 0f;
        timer = 0;
    }

    public void Update()
    {
        timer += Time.deltaTime;

        if (timer >= hurtAnimationDuration)
        {
            if (slime.CanSeePlayer())
            {
                slime.Animator.SetBool("isChasing", true);
                slime.StartChase();
                return;
            }
            else
                slime.StartIdle();
        }

    }

    public void Exit() { }
}