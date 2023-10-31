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

        //Idle상태는 어느 상태에서도 진입가능하기 때문에 true로 한다.
        public override bool canExecute => machine.currentType == StateType.Idle ||
                                            machine.currentType == StateType.Move;

        //None에서 idle상태를 실행하기 위해 필요한 것들을 지정해둔다
        //idle는 다른 상태로 이전되기 전까지 끝나지 않는 행동이기 때문에 WaitUntilActionFinished에서 지속되게 한다.
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