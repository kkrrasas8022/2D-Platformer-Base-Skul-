using Skul.Character;
using UnityEngine;

namespace Skul.FSM.States
{
    public class StateIdle : State
    {
        GroundDetecter groundDetecter;
        public StateIdle(StateMachine machine) : base(machine)
        {
            groundDetecter=machine.GetComponent<GroundDetecter>();
        }

        //Idle상태는 어느 상태에서도 진입가능하기 때문에 true로 한다.
        public override bool canExecute => true;

        //None에서 idle상태를 실행하기 위해 필요한 것들을 지정해준다
        //idle는 다른 상태로 변환되기 전까지 끝나지 않는 행동이기 때문에 WaitUntilActionFinished에서 지속되게 한다.
        public override StateType MoveNext()
        {
            //Debug.Log("StateIdle");
            StateType next = StateType.Idle;
            switch (currentStep)
            {
                case IStateEnumerator<StateType>.Step.None:
                    {
                        character.JumpCount=0;
                        character.DashCount=0;
                        movement.isMovable=true;
                        movement.isDirectionChangeable = true;
                        animator.Play("Idle");
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
                        if(groundDetecter.isDetected==false)
                            next=StateType.Fall;
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