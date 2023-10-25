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

        //Die���´� ��� ���¿����� ���԰����ϱ� ������ true�� �Ѵ�.
        public override bool canExecute => true;

        //None���� Die���¸� �����ϱ� ���� �ʿ��� �͵��� �����صд�
        //Die�� ���°� ������ ��ü�� ������� �Ѵ�.
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