using Skul.Character;
using System;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

namespace Skul.FSM.States
{
    public class StateFall : State
    {
        GroundDetecter groundDetecter;
        public StateFall(StateMachine machine) : base(machine)
        {
            groundDetecter=machine.GetComponent<GroundDetecter>();
        }

        //Idle���´� ��� ���¿����� ���԰����ϱ� ������ true�� �Ѵ�.
        public override bool canExecute =>true;

        //None���� idle���¸� �����ϱ� ���� �ʿ��� �͵��� �����صд�
        //idle�� �ٸ� ���·� �����Ǳ� ������ ������ �ʴ� �ൿ�̱� ������ WaitUntilActionFinished���� ���ӵǰ� �Ѵ�.
        public override StateType MoveNext()
        {
            //Debug.Log("StateFall");
            StateType next = StateType.Fall;
            switch (currentStep)
            {
                case IStateEnumerator<StateType>.Step.None:
                    {
                        movement.isMovable=true;
                        movement.isDirectionChangeable = true;
                        animator?.Play("Fall");
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
                        if(groundDetecter.isDetected==true)
                        {
                            next = movement.horizontal==0 ?StateType.Idle:StateType.Move;
                        }
                    }
                    break;
                case IStateEnumerator<StateType>.Step.Finish:
                    break;
                default:
                    break;
            }
            return next;
        }
    }
}