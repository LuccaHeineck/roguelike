using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class SlimeDeadState : IState
{
    private SlimeControl slime;
    private const float deadAnimationDuration = 0.67f;
    private float timer;

    public SlimeDeadState(SlimeControl slime) => this.slime = slime;

    public void Enter()
    {
        slime.Rb.linearVelocity = Vector2.zero;
        slime.Animator.SetBool("isDead", true);
        slime.Animator.SetBool("isWalking", false);
        slime.Animator.SetBool("isChasing", false);
        slime.Animator.SetBool("takingDamage", false);

        timer = 0;
    }

    public void Update()
    {
        timer += Time.deltaTime;

        if (timer >= deadAnimationDuration)
        {
            Object.Destroy(slime.gameObject);
        }

    }

    public void Exit() { }
}