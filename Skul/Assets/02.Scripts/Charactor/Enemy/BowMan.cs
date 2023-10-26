using System;
using System.Collections.Generic;
using Skul.FSM;
using Skul.FSM.States;

namespace Skul.Character.Enemy
{
    public class BowMan:Enemy
    {
        protected override void Start()
        {
            base.Start();
            stateMachine.InitStates(new Dictionary<StateType, IStateEnumerator<StateType>>()
            {
                { StateType.Idle,   new StateIdle(stateMachine)},
                { StateType.Move,   new StateMove(stateMachine)},
                { StateType.Hurt,   new StateHurt(stateMachine)},
                { StateType.Attack, new StateAttack(stateMachine)},
                { StateType.Die,    new StateDie(stateMachine)},
                { StateType.Skill_1,   new StateSkill_1(stateMachine)},
            });
        }
    }
}