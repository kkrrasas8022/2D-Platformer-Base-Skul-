using Skul.FSM.States;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Skul.Character.PC
{
    public class Normal:player
    {
        private void Start()
        {
            stateMachine.InitStates(new Dictionary<FSM.StateType, FSM.IStateEnumerator<FSM.StateType>>()
            {
                { FSM.StateType.Idle,new FSM.States.StateIdle(stateMachine) },
                { FSM.StateType.Move,new FSM.States.StateMove(stateMachine) },
                { FSM.StateType.Attack,new FSM.States.StateAttack(stateMachine) },
                { FSM.StateType.Dash,new FSM.States.StateDash(stateMachine) },
                { FSM.StateType.Jump,new FSM.States.StateJump(stateMachine) },
                { FSM.StateType.DownJump,new FSM.States.StateDownJump(stateMachine) },
                { FSM.StateType.Fall,new FSM.States.StateFall(stateMachine) },
                { FSM.StateType.Hurt,new FSM.States.StateHurt(stateMachine) },
                { FSM.StateType.Skill_1,new FSM.States.StateSkill_1(stateMachine) },
                { FSM.StateType.Skill_2,new FSM.States.StateSkill_2(stateMachine) },
                { FSM.StateType.Die,new FSM.States.StateDie(stateMachine) },
                { FSM.StateType.JumpAttack,new FSM.States.StateJumpAttack(stateMachine) },
                { FSM.StateType.Switch,new FSM.States.StateSwitch(stateMachine) },
            });
        }

        private void Skill_1()
        {
            Debug.Log("Skill_1");
        }

        private void Skill_2()
        {
            Debug.Log("Skill_2");
        }

        private void Attack_Hit()
        {
            Debug.Log("Hit");
        }
    }
}