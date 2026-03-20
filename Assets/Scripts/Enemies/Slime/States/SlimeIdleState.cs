using UnityEngine;

public class SlimeIdleState : IState
{
    private SlimeControl slime;
    private const float idleDuration = 5f;
    private float timer;

    public SlimeIdleState(SlimeControl slime) => this.slime = slime;

    public void Enter()
    {
        slime.Agent.speed = 0f;
        timer = 0;
    }

    public void Update()
    {
        if (slime.CloseToPlayer())
        {
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

    public void Exit() { }
}