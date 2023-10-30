using Skul.FSM.States;
using Skul.FSM;
using System;
using System.Collections.Generic;
using UnityEngine;
using Skul.Movement;

namespace Skul.Character.PC
{
    public class Normal:PlayerAttacks
    {
        [SerializeField]Collider2D col;
       [SerializeField] private Vector3 _hitSize;
        [SerializeField] private Vector3 _hitOffset;
        [SerializeField] private Vector3 _switchHitSize;
        [SerializeField] private Vector3 _switchHitOffset;
        protected override void SwitchAttack()
        {
            base.SwitchAttack();
        }
        protected override void JumpAttack()
        {
            base.JumpAttack();
        }
        protected override void Skill_1()
        {
            base.Skill_1();
        }

        protected override void Skill_2()
        {
            base.Skill_2();
        }

        protected override void Attack_Hit()
        {
            base.Attack_Hit();
           col=
            Physics2D.OverlapBox((Vector2)_player.transform.position + new Vector2(_hitOffset.x*_movement.direction,
            
                                                                            _hitOffset.y),
                                 _hitSize,
                                 0.0f,
                                 _enemyMask);

            if (col && col.TryGetComponent(out IHp ihp))
            {
                ihp.Damage(_player.gameObject, _player.attackForce);
                //DamagePopUp.Create(_attackTargetMask, col.transform.position + Vector3.up * .2f, (int)player.attackForce);
            }
        }

        private void OnDrawGizmos()
        {
            
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(_player.transform.position+new Vector3(_hitOffset.x*_movement.direction,_hitOffset.y), _hitSize);
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(_player.transform.position + _switchHitOffset, _switchHitSize);
        }
    }
}