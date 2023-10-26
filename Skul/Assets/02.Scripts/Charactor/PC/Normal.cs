using Skul.FSM.States;
using Skul.FSM;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Skul.Character.PC
{
    public class Normal:player
    {
        protected override void Start()
        {
            base.Start();
            stateMachine.InitStates(new Dictionary<StateType, IStateEnumerator<StateType>>()
            {
                { StateType.Idle,       new StateIdle(stateMachine) },
                { StateType.Move,       new StateMove(stateMachine) },
                { StateType.Attack,     new StateAttack(stateMachine) },
                { StateType.Dash,       new StateDash(stateMachine) },
                { StateType.Jump,       new StateJump(stateMachine) },
                { StateType.DownJump,   new StateDownJump(stateMachine) },
                { StateType.Fall,       new StateFall(stateMachine) },
                { StateType.Hurt,       new StateHurt(stateMachine) },
                { StateType.Skill_1,    new StateSkill_1(stateMachine) },
                { StateType.Skill_2,    new StateSkill_2(stateMachine) },
                { StateType.Die,        new StateDie(stateMachine) },
                { StateType.JumpAttack, new StateJumpAttack(stateMachine) },
                { StateType.Switch,     new StateSwitch(stateMachine) },
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