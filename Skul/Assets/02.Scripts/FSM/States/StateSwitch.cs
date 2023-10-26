using System;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

namespace Skul.FSM.States
{
    public class StateSwitch : State
    {
        public StateSwitch(StateMachine machine) : base(machine)
        {
        }

        //Idle���´� ��� ���¿����� ���԰����ϱ� ������ true�� �Ѵ�.
        public override bool canExecute => true;

        //None���� idle���¸� �����ϱ� ���� �ʿ��� �͵��� �����صд�
        //idle�� �ٸ� ���·� �����Ǳ� ������ ������ �ʴ� �ൿ�̱� ������ WaitUntilActionFinished���� ���ӵǰ� �Ѵ�.
        public override StateType MoveNext()
        {
            Debug.Log("StateAttack");
            StateType next = StateType.Attack;
            switch (currentStep)
            {
                case IStateEnumerator<StateType>.Step.None:
                    {
                        movement.isMovable=true;
                        movement.isDirectionChangeable = true;
                        animator.Play("Switch");
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
                        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 2.0f)
                        {
                            currentStep++;
                        }
                    }
                    break;
                case IStateEnumerator<StateType>.Step.Finish:
                    {
                        next = movement.horizontal == 0 ? StateType.Idle : StateType.Move;
                    }
                    break;
                default:
                    break;
            }
            return next;
        }
    }
}