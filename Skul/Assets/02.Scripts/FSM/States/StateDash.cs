using Skul.Character;
using Skul.Character.PC;
using System;
using System.Collections;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

namespace Skul.FSM.States
{
    public class StateDash : State
    {
        GroundDetecter groundDetecter;
        public StateDash(StateMachine machine) : base(machine)
        {
            groundDetecter=machine.GetComponent<GroundDetecter>();
            
        }
        
        //어느상태에서든 진입가능하기 때문에 true로 한다.
        public override bool canExecute => character.dashCount<character.maxDashCount;

        //None에서 Dash상태를 실행하기 위해 필요한 것들을 지정해준다
        //Dash는 끝날때 이동 입력이 있다면 move,입력이 없다면 idle로 전환 해준다
        public override StateType MoveNext()
        {
            StateType next = StateType.Dash;
            Vector2 saveVelocity=Vector2.zero;
            switch (currentStep)
            {
                case IStateEnumerator<StateType>.Step.None:
                    {
                        character.dashCount++;
                        movement.isMovable=false;
                        movement.isDirectionChangeable = false;
                        collider.enabled=false;
                        animator.Play("Dash");
                        rigid.velocity = Vector2.zero;
                        rigid.gravityScale = 0;
                        rigid.AddForce((movement.direction == 1 ? Vector2.right : Vector2.left)
                            * ((Player)character).dashForce, ForceMode2D.Impulse);
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
                        //다음 단계로 가기까지 FSM을 잠시 멈춘다
                        if (machine.times < machine.StopTime)
                        { 
                            machine.times++;
                            break;
                        }
                        machine.times = 0;
                        currentStep++;
                    }
                    break;
                case IStateEnumerator<StateType>.Step.WaitUntilActionFinished:
                    {
                        collider.enabled = true;
                        rigid.velocity= Vector2.zero;
                        rigid.gravityScale = 1;
                        currentStep++;
                    }
                    break;
                case IStateEnumerator<StateType>.Step.Finish:
                    {
                        next = groundDetecter.isDetected?(movement.horizontal == 0.0f ? StateType.Idle : StateType.Move):StateType.Fall; 
                    }
                    break;
                default:
                    break;
            }
            return next;
        }
    }
}