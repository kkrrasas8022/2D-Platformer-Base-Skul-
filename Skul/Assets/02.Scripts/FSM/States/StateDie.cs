using System;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

namespace Skul.FSM.States
{
    public class StateDie : State
    {
        public StateDie(StateMachine machine) : base(machine)
        {
        }

        //Die상태는 어느 상태에서도 진입가능하기 때문에 true로 한다.
        public override bool canExecute => true;

        //None에서 Die상태를 실행하기 위해 필요한 것들을 지정해둔다
        //Die는 상태가 끝나면 객체를 사라지게 한다.
        public override StateType MoveNext()
        {
            StateType next = StateType.Die;
            switch (currentStep)
            {
                case IStateEnumerator<StateType>.Step.None:
                    {
                        movement.isMovable=false;
                        movement.isDirectionChangeable = false;
                        animator.Play("Die");
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
                        GameObject.Destroy(machine.gameObject);
                    }
                    break;
                default:
                    break;
            }
            return next;
        }
    }
}