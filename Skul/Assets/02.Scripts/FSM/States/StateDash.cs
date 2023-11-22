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
        
        //������¿����� ���԰����ϱ� ������ true�� �Ѵ�.
        public override bool canExecute => character.dashCount<character.maxDashCount;

        //None���� Dash���¸� �����ϱ� ���� �ʿ��� �͵��� �������ش�
        //Dash�� ������ �̵� �Է��� �ִٸ� move,�Է��� ���ٸ� idle�� ��ȯ ���ش�
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
                        //���� �ܰ�� ������� FSM�� ��� �����
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