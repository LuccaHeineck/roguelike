using UnityEngine;

// public class SlimeIdleState : IState
// {
//     private EnemyControl Slime;

//     public SlimeIdleState(EnemyControl slime) => this.slime = slime;

//     public void Enter()
//     {
//         slime.Rb.linearVelocity = Vector2.zero;
//         slime.Animator.SetBool("isRunning", false);
//         slime.Animator.SetFloat("lastInputX", slime.LastMoveInput.x);
//         slime.Animator.SetFloat("lastInputY", slime.LastMoveInput.y);
//     }

//     public void Update()
//     {
//         if (slime.AttackPressed)
//         {
//             slime.ConsumeAttack();
//             slime.StateMachine.ChangeState(new slimeAttackState(slime));
//             return;
//         }

//         if (slime.MoveInput != Vector2.zero)
//             slime.StateMachine.ChangeState(new slimeChaseState(slime));
//     }

//     public void Exit() { }
// }