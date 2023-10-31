using System;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

namespace Skul.FSM.States
{
    public class StateJumpAttack : State
    {
        public StateJumpAttack(StateMachine machine) : base(machine)
        {
        }

        //Idle상태는 어느 상태에서도 진입가능하기 때문에 true로 한다.
        public override bool canExecute => machine.currentType==StateType.Jump||
                                                        machine.currentType==StateType.Fall;

        //None에서 idle상태를 실행하기 위해 필요한 것들을 지정해둔다
        //idle는 다른 상태로 이전되기 전까지 끝나지 않는 행동이기 때문에 WaitUntilActionFinished에서 지속되게 한다.
        public override StateType MoveNext()
        {
            //Debug.Log("StateJumpAttack");
            StateType next = StateType.JumpAttack;
            switch (currentStep)
            {
                case IStateEnumerator<StateType>.Step.None:
                    {
                        movement.isMovable=false;
                        movement.isDirectionChangeable = false;
                        animator.Play("JumpAttack");
                        currentStep++;
                    }
                    break;
                case IStateEnumerator<StateType>.Step.Start:
                    {
                        currentStep++;
                    }
                    break;
                case IStateEnumerator<StateType>.Step.Casting:
                    {
                        currentStep++;
                    }
                    break;
                case IStateEnumerator<StateType>.Step.DoAction:
                    {
                        currentStep++;
                    }
                    break;
                case IStateEnumerator<StateType>.Step.WaitUntilActionFinished:
                    {
                        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
                        {
                            currentStep++;
                        }
                    }
                    break;
                case IStateEnumerator<StateType>.Step.Finish:
                    {
                        next=rigid.velocity.y>0?StateType.Jump:StateType.Fall;
                    }
                    break;
                default:
                    break;
            }
            return next;
        }
    }
}