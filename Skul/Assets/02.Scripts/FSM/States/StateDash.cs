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

        //어느상태에서든 진입가능하기 때문에 true로 한다.
        public override bool canExecute => true;

        //None에서 Dash상태를 실행하기 위해 필요한 것들을 지정해준다
        //Dash는 끝날때 이동 입력이 있다면 move,입력이 없다면 idle로 전환 해준다
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