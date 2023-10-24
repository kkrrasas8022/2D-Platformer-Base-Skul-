using System;
using System.Collections.Generic;

namespace Skul.Character
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
            });
        }
    }
}