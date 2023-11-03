using System;
using System.Collections.Generic;
using Skul.FSM;
using Skul.FSM.States;
using UnityEngine;

namespace Skul.Character.Enemy
{
    public class BowMan_Enemy:Enemy
    {
        [SerializeField] private EnemyProjectile _arrow;
        [SerializeField] private GameObject _arrowLine;
        [SerializeField] private LayerMask _targetMask;
        [SerializeField] private float _arrowVelocity;


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
            Instantiate(_arrow,
                   transform.position + new Vector3(movement.direction * 0.1f, 0.5f, 0.0f),
                   Quaternion.Euler(new Vector3(0,movement.direction==1?180:0,90)))
            .SetUp(gameObject, attackForce, _targetMask, new Vector2(movement.direction*_arrowVelocity, 0));
        }

    }
}