using Skul.FSM.States;
using Skul.FSM;
using System;
using System.Collections.Generic;
using UnityEngine;
using Skul.Movement;
using UnityEditor.Animations;
using Skul.Data;
using Skul.Character.Enemy;
using Skul.UI;

namespace Skul.Character.PC
{
    public class Minotaurus:PlayerAttacks
    { 
        [SerializeField] private Collider2D _lastEnemy;
        [SerializeField] private Collider2D[] _lastEnemies;
        [SerializeField] private Vector3 _hit1Size;
        [SerializeField] private Vector3 _hit1Offset;
        [SerializeField] private Vector3 _hit2Size;
        [SerializeField] private Vector3 _hit2Offset;
        [SerializeField] private Vector3 _jumpHitSize;
        [SerializeField] private Vector3 _jumpHitOffset;

        [SerializeField] private BuffData _buffData;
        [SerializeField] private Buff _buff;

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
            Instantiate(_buff, MainUI.instance.transform).SetUp(_buffData);
        }
        protected override void JumpAttack()
        {
            base.JumpAttack();
            _lastEnemies =
                       Physics2D.OverlapBoxAll((Vector2)_player.transform.position + new Vector2(_jumpHitOffset.x * _movement.direction,

                                                                           _jumpHitOffset.y),
                                _jumpHitSize,
                                0.0f,
                                _enemyMask);


            if (_lastEnemies.Length > 0)
            {
                for (int i = 0; i < _lastEnemies.Length; i++)
                {
                    if (_lastEnemies[i].TryGetComponent(out IHp ihp))
                    {
                        ihp.Damage(_player.gameObject, _player.AttackForce * 1.5f, out float a);
                    }
                }
            }
        }
        protected override void Skill(int skillID)
        {
            base.Skill(skillID);
            switch(skillID) 
            {
                case 1007:
                    {
                        _lastEnemies =
                        Physics2D.OverlapBoxAll((Vector2)_player.transform.position + new Vector2(_hit2Offset.x * _movement.direction,

                                                                            _hit2Offset.y),
                                 _hit2Size,
                                 0.0f,
                                 _enemyMask);


                        if (_lastEnemies.Length > 0)
                        {
                            for (int i = 0; i < _lastEnemies.Length; i++)
                            {
                                if (_lastEnemies[i].TryGetComponent(out IHp ihp))
                                {
                                    ihp.Damage(_player.gameObject, _player.AttackForce*1.5f, out float a);
                                }
                            }
                        }
                    }
                    break;
                case 1008:
                    {
                        _lastEnemies =
                Physics2D.OverlapBoxAll((Vector2)_player.transform.position + new Vector2(_hit1Offset.x * _movement.direction,

                                                                             _hit1Offset.y),
                                  _hit1Size,
                                 0.0f,
                                 _enemyMask);


                        if (_lastEnemies.Length > 0)
                        {
                            for (int i = 0; i < _lastEnemies.Length; i++)
                            {
                                if (_lastEnemies[i].TryGetComponent(out IHp ihp))
                                {
                                    ihp.Damage(_player.gameObject, _player.AttackForce*1.5f, out float a);
                                }
                            }
                        }
                    }
                    break;
            }
           

        }



        protected override void Attack_Hit()
        {
            base.Attack_Hit();

            int count = _player.nowComboCount;
            _lastEnemies =
                Physics2D.OverlapBoxAll((Vector2)_player.transform.position + new Vector2((count==1?_hit1Offset:_hit2Offset).x * _movement.direction,

                                                                            (count == 1 ? _hit1Offset : _hit2Offset).y),
                                 (count == 1 ? _hit1Size: _hit2Size),
                                 0.0f,
                                 _enemyMask);

        
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
            Gizmos.DrawWireCube(_player.transform.position+new Vector3(_hit1Offset.x*_movement.direction,_hit1Offset.y), _hit1Size); 
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(_player.transform.position + new Vector3(_hit2Offset.x * _movement.direction, _hit2Offset.y), _hit2Size);
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(_player.transform.position + _jumpHitOffset, _jumpHitSize);
        }
    }
}