using System;
using UnityEngine;
using Unity.PlasticSCM.Editor.WebApi;
using Skul.Character;

namespace Skul.FSM.States
{
    public class StateDownJump : State
    {
        GroundDetecter groundDetecter;
        Collider2D save;
        int ignortime = 0;
        int ignorMaxTime = 120;
        public StateDownJump(StateMachine machine) : base(machine)
        {
            groundDetecter=machine.GetComponent<GroundDetecter>();
        }

        //Idle���´� ��� ���¿����� ���԰����ϱ� ������ true�� �Ѵ�.
        public override bool canExecute => machine.currentType == StateType.Idle ||
                                            machine.currentType == StateType.Move;

        //None���� idle���¸� �����ϱ� ���� �ʿ��� �͵��� �����صд�
        //idle�� �ٸ� ���·� �����Ǳ� ������ ������ �ʴ� �ൿ�̱� ������ WaitUntilActionFinished���� ���ӵǰ� �Ѵ�.
        public override StateType MoveNext()
        {
            //Debug.Log("StateDownJump");
            StateType next = StateType.DownJump;
            ignortime++;
            switch (currentStep)
            {
                case IStateEnumerator<StateType>.Step.None:
                    {
                        movement.isMovable=true;
                        movement.isDirectionChangeable = true;
                        save = groundDetecter.detected;
                        save.enabled=false;
                        currentStep++;
                        ignortime = 0;
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
                        if (ignortime >= ignorMaxTime)
                        { 
                            save.enabled = true; 
                            next= StateType.Fall;
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