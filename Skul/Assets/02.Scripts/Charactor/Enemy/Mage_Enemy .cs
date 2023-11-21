using System;
using System.Collections.Generic;
using Skul.FSM;
using Skul.FSM.States;
using UnityEngine;

namespace Skul.Character.Enemy
{
    public class Mage_Enemy:Enemy
    {
        [SerializeField] private EnemyProjectile _arrow;
        [SerializeField] private float _arrowVelocity;
        [SerializeField] private EnemyAI _enemyAI;
        [SerializeField] private EnemyProjectile _nowArrow;

        protected override void Awake()
        {
            base.Awake();
            _enemyAI=GetComponent<EnemyAI>();
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

        private void MakeIce()
        {
            Debug.Log("MageManHit");
            _nowArrow = Instantiate(_arrow,
                   _enemyAI.target.transform.position + (Vector3.up * 6.5f),
                   Quaternion.identity);
            _nowArrow.SetUp(gameObject, attackForce, _targetMask, Vector2.zero);
        }

        private void Shot()
        {
            _nowArrow.SetUp(gameObject, attackForce, _targetMask, Vector2.down*2);
        }

    }
}