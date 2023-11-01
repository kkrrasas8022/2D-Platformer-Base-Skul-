using Skul.Character;
using System;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

namespace Skul.FSM.States
{
    public class StateMove : State
    {
        GroundDetecter groundDetecter;
        public StateMove(StateMachine machine) : base(machine)
        {
            groundDetecter=machine.GetComponent<GroundDetecter>();
        }

        //Move���´� ��� ���¿����� ���԰����ϱ� ������ true�� �Ѵ�.
        public override bool canExecute => machine.currentType != StateType.Skill_1 && machine.currentType != StateType.Skill_2;

        //None���� Move���¸� �����ϱ� ���� �ʿ��� �͵��� �������ش�
        //Move�� �ٸ� ���·� ��ȯ�Ǳ� ������ ������ �ʴ� �ൿ�̱� ������ WaitUntilActionFinished���� ���ӵǰ� �Ѵ�.
        public override StateType MoveNext()
        {
            //Debug.Log("StateMove");
            StateType next = StateType.Move;
            switch (currentStep)
            {
                case IStateEnumerator<StateType>.Step.None:
                    {
                        character.JumpCount = 0 ;
                        character.DashCount = 0 ;
                        movement.isMovable=true;
                        movement.isDirectionChangeable = true;
                        animator.Play("Move");
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
                        if (groundDetecter.isDetected == false)
                            next = StateType.Fall;
                        if (movement.horizontal ==0)
                            next= StateType.Idle;
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