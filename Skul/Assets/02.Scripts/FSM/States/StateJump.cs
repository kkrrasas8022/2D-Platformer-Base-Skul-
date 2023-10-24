using System;
using UnityEngine;
using Unity.PlasticSCM.Editor.WebApi;
using Skul.Character;

namespace Skul.FSM.States
{
    public class StateJump : State
    {
        GroundDetecter groundDetecter;
        public StateJump(StateMachine machine) : base(machine)
        {
            groundDetecter = machine.GetComponent<GroundDetecter>();
        }

        //Idle상태는 어느 상태에서도 진입가능하기 때문에 true로 한다.
        public override bool canExecute => machine.currentType==StateType.Idle||
                                            machine.currentType==StateType.Move;

        //None에서 idle상태를 실행하기 위해 필요한 것들을 지정해둔다
        //idle는 다른 상태로 이전되기 전까지 끝나지 않는 행동이기 때문에 WaitUntilActionFinished에서 지속되게 한다.
        public override StateType MoveNext()
        {
            Debug.Log("StateJump");
            StateType next = StateType.Jump;
            switch (currentStep)
            {
                case IStateEnumerator<StateType>.Step.None:
                    {
                        //movement.isMovable=true;
                        //movement.isDirectionChangeable = true;
                        //rigid.velocity = new Vector2(rigid.velocity.x, 0.0f);
                        //rigid.AddForce(Vector2.up * character.jumpForce, ForceMode2D.Impulse);
                        ////animation
                        currentStep++;
                    }
                    break;
                case IStateEnumerator<StateType>.Step.Start:
                    {
                        movement.isMovable = true;
                        movement.isDirectionChangeable = true;
                        rigid.velocity = new Vector2(rigid.velocity.x, 0.0f);
                        rigid.AddForce(Vector2.up * character.jumpForce, ForceMode2D.Impulse);
                        //animation
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
                        Debug.Log("Waitun");
                        if(rigid.velocity.y <= 0)
                            currentStep++;
                    }
                    break;
                case IStateEnumerator<StateType>.Step.Finish:
                    {
                        Debug.Log("Finish");
                        if (groundDetecter.isDetected)
                        {
                            Debug.Log("Movech");
                            next = movement.horizontal == 0.0f ? StateType.Idle : StateType.Move;
                        }
                        else
                        {
                            Debug.Log("MoveFall");
                            next = StateType.Fall;
                        }
                    }
                    break;
                default:
                    break;
            }
            return next;
        }
    }
}