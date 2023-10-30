using System;
using System.Collections.Generic;
using Skul.FSM;
using Skul.FSM.States;
using UnityEngine;

namespace Skul.Character.Enemy
{
    public class BowMan_Enemy:Enemy
    {
        protected override void Awake()
        {
            base.Awake();

        }
        protected override void Start()
        {
            base.Start();
            stateMachine.InitStates(new Dictionary<StateType, IStateEnumerator<StateType>>()
            {
                { StateType.Idle,   new StateIdle(stateMachine)},
                { StateType.Move,   new StateMove(stateMachine)},
                { StateType.Fall,   new StateFall(stateMachine)},
                { StateType.Hurt,   new StateHurt(stateMachine)},
                { StateType.Attack, new StateAttack(stateMachine)},
                { StateType.Die,    new StateDie(stateMachine)},
            });
        }

        private void Hit()
        {
            Debug.Log("BowManHit");
        }
    }
}