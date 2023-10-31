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

        //Idle���´� ��� ���¿����� ���԰����ϱ� ������ true�� �Ѵ�.
        public override bool canExecute => true;

        //None���� idle���¸� �����ϱ� ���� �ʿ��� �͵��� �������ش�
        //idle�� �ٸ� ���·� ��ȯ�Ǳ� ������ ������ �ʴ� �ൿ�̱� ������ WaitUntilActionFinished���� ���ӵǰ� �Ѵ�.
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