using System;
using System.Collections.Generic;
using Skul.FSM;
using Skul.FSM.States;
using Skul.Movement;
using UnityEngine;

namespace Skul.Character.Enemy
{
    public class Sword_Enemy:Enemy
    {
        [SerializeField] Collider2D col;
        [SerializeField] private Vector3 _hitSize;
        [SerializeField] private Vector3 _hitOffset;
        


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
            Debug.Log("SwordManHit");
            col =
            Physics2D.OverlapBox((Vector2)transform.position + new Vector2(_hitOffset.x * movement.direction,

                                                                            _hitOffset.y),
                                 _hitSize,
                                 0.0f,
                                 _targetMask);

            if (col && col.TryGetComponent(out IHp ihp))
            {
                ihp.Damage(gameObject, attackForce);
                //DamagePopUp.Create(_attackTargetMask, col.transform.position + Vector3.up * .2f, (int)player.attackForce);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(transform.position + new Vector3(_hitOffset.x * movement.direction, _hitOffset.y),_hitSize);
        }
    }
}