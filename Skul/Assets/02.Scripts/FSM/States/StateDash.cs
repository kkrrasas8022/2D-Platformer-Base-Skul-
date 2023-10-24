using Skul.Character;
using System;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

namespace Skul.FSM.States
{
    public class StateDash : State
    {
        public StateDash(StateMachine machine) : base(machine)
        {
        }

        //������¿����� ���԰����ϱ� ������ true�� �Ѵ�.
        public override bool canExecute => true;

        //None���� Dash���¸� �����ϱ� ���� �ʿ��� �͵��� �������ش�
        //Dash�� ������ �̵� �Է��� �ִٸ� move,�Է��� ���ٸ� idle�� ��ȯ ���ش�
        public override StateType MoveNext()
        {
            StateType next = StateType.Dash;
            switch (currentStep)
            {
                case IStateEnumerator<StateType>.Step.None:
                    {
                        //movement.isMovable=false;
                        movement.isDirectionChangeable = false;
                        //animation
                        rigid.velocity = Vector2.zero;
                        rigid.gravityScale = 0;
                        rigid.AddForce((movement.direction == 1 ? Vector2.right : Vector2.left)
                            * ((player)character).dashForce, ForceMode2D.Impulse);
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
                            currentStep++;
                    }
                    break;
                case IStateEnumerator<StateType>.Step.Finish:
                    {
                        next = movement.horizontal == 0.0f ? StateType.Idle : StateType.Move; 
                    }
                    break;
                default:
                    break;
            }
            return next;
        }
    }
}