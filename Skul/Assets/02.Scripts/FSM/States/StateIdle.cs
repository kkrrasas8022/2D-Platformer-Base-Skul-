using UnityEngine;

namespace Skul.FSM.States
{
    public class StateIdle : State
    {
        public StateIdle(StateMachine machine) : base(machine)
        {
        }

        //Idle���´� ��� ���¿����� ���԰����ϱ� ������ true�� �Ѵ�.
        public override bool canExecute => true;

        //None���� idle���¸� �����ϱ� ���� �ʿ��� �͵��� �������ش�
        //idle�� �ٸ� ���·� ��ȯ�Ǳ� ������ ������ �ʴ� �ൿ�̱� ������ WaitUntilActionFinished���� ���ӵǰ� �Ѵ�.
        public override StateType MoveNext()
        {
            Debug.Log("StateIdle");
            StateType next = StateType.Idle;
            switch (currentStep)
            {
                case IStateEnumerator<StateType>.Step.None:
                    {
                        movement.isMovable=true;
                        movement.isDirectionChangeable = true;
                        //animation
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