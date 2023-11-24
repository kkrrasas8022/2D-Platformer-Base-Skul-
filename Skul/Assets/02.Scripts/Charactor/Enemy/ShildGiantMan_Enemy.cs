using System;
using System.Collections.Generic;
using System.Collections;
using Skul.FSM;
using Skul.FSM.States;
using Skul.Movement;
using UnityEngine;

namespace Skul.Character.Enemy
{
    public class ShildGiant_Enemy:Enemy
    {
        [SerializeField] private Collider2D coll;
        [SerializeField] Collider2D trig;
        [SerializeField] private Vector3 _hitSize;
        [SerializeField] private Vector3 _hitOffset;
        [SerializeField] private float _chargeForce;
        [SerializeField] private bool isCharging;
        [SerializeField] private Animator animator;
        [SerializeField] private WaitForSeconds wait=new WaitForSeconds(1.0f);


        protected override void Awake()
        {
            base.Awake();
            animator = GetComponent<Animator>();
        }
        protected override void Start()
        {
            base.Start();
            stateMachine.InitStates(new Dictionary<StateType, IStateEnumerator<StateType>>()
            {
                { StateType.Idle,   new StateIdle(stateMachine)},
                { StateType.Move,   new StateMove(stateMachine)},
                { StateType.Fall,   new StateFall(stateMachine)},
                { StateType.Attack, new StateAttack(stateMachine)},
                { StateType.Die,    new StateDie(stateMachine)},
            });
        }

        private void StartCharging()
        {
            isCharging = true;
            rigid.gravityScale = 0;
            coll.enabled = false;
            rigid.AddForce((movement.direction==1?Vector2.right:Vector2.left) * _chargeForce, ForceMode2D.Impulse);
            StartCoroutine(Charging());
        }

        private IEnumerator Charging()
        {
            animator.Play("Charging");
            yield return wait;
            rigid.velocity= Vector2.zero;
            coll.enabled=true;
            rigid.gravityScale = 1;
            isCharging = false;
        }

        private void Hit()
        {
            if (trig && trig.TryGetComponent(out IHp ihp))
            {
                ihp.Damage(gameObject, attackForce, out float damage);
                //DamagePopUp.Create(_attackTargetMask, col.transform.position + Vector3.up * .2f, (int)player.attackForce);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player")&&isCharging)
            {
                trig = collision;
                Hit();
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(transform.position + new Vector3(_hitOffset.x * movement.direction, _hitOffset.y),_hitSize);
        }
    }
}