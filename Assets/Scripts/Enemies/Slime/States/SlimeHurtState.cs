using UnityEngine;

public class SlimeHurtState : IState
{
    private SlimeControl slime;
    private const float hurtAnimationDuration = 0.33f;
    private float timer;

    public SlimeHurtState(SlimeControl slime) => this.slime = slime;

    public void Enter()
    {
        slime.Animator.SetTrigger("takingDamage");

        slime.Agent.speed = 0f;
        timer = 0;
    }

    public void Update()
    {
        timer += Time.deltaTime;

        if (timer >= hurtAnimationDuration)
        {
            if (slime.CanSeePlayer())
            {
                slime.StartChase();
                return;
            }

            slime.StartIdle();
        }

    }

    public void Exit() { }
}