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

        //Idle���´� ��� ���¿����� ���԰����ϱ� ������ true�� �Ѵ�.
        public override bool canExecute => machine.currentType==StateType.Jump||
                                                        machine.currentType==StateType.Fall;

        //None���� idle���¸� �����ϱ� ���� �ʿ��� �͵��� �����صд�
        //idle�� �ٸ� ���·� �����Ǳ� ������ ������ �ʴ� �ൿ�̱� ������ WaitUntilActionFinished���� ���ӵǰ� �Ѵ�.
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