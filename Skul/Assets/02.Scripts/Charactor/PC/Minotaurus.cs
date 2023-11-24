using Skul.FSM.States;
using Skul.FSM;
using System;
using System.Collections.Generic;
using UnityEngine;
using Skul.Movement;
using UnityEditor.Animations;
using Skul.Data;
using Skul.Character.Enemy;

namespace Skul.Character.PC
{
    public class Minotaurus:PlayerAttacks
    { 
        [SerializeField] private Collider2D _lastEnemy;
        [SerializeField] private Collider2D[] _lastEnemies;
        [SerializeField] private Vector3 _hitSize;
        [SerializeField] private Vector3 _hitOffset;
        [SerializeField] private Vector3 _switchHitSize;
        [SerializeField] private Vector3 _switchHitOffset;




        private void Update()
        {
    
            
        }

        protected override void Awake()
        {
            base.Awake();

        }

        protected override void Start()
        {
            base.Start();
        }
        private void OnEnable()
        {

        }

        protected override void SwitchAttack()
        {
            base.SwitchAttack();

        }
        protected override void JumpAttack()
        {
            base.JumpAttack();

        }
        protected override void Skill(int skillID)
        {
            base.Skill(skillID);

           

        }

        

        protected override void Attack_Hit()
        {
            base.Attack_Hit();
            //_lastEnemy=
            //Physics2D.OverlapBox((Vector2)_player.transform.position + new Vector2(_hitOffset.x*_movement.direction,
            //
            //                                                                _hitOffset.y),
            //                     _hitSize,
            //                     0.0f,
            //                     _enemyMask);

            _lastEnemies=
                Physics2D.OverlapBoxAll((Vector2)_player.transform.position + new Vector2(_hitOffset.x * _movement.direction,

                                                                            _hitOffset.y),
                                 _hitSize,
                                 0.0f,
                                 _enemyMask);

            //if (_lastEnemy && _lastEnemy.TryGetComponent(out IHp ihp))
            //{
            //    ihp.Damage(_player.gameObject, _player.AttackForce);
            //    //DamagePopUp.Create(_attackTargetMask, col.transform.position + Vector3.up * .2f, (int)player.attackForce);
            //}
            if (_lastEnemies.Length>0)
            {
                for(int i=0;i<_lastEnemies.Length;i++)
                {
                    if (_lastEnemies[i].TryGetComponent(out IHp ihp))
                    { 
                        ihp.Damage(_player.gameObject, _player.AttackForce,out float a);
                    }
                }
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