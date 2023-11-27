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
    public class Normal:PlayerAttacks
    { 
        [SerializeField] private Collider2D _lastEnemy;
        [SerializeField] private Collider2D[] _lastEnemies;
        [SerializeField] private Vector3 _hitSize;
        [SerializeField] private Vector3 _hitOffset;
        [SerializeField] private Vector3 _jumpHitSize;
        [SerializeField] private Vector3 _jumpHitOffset;
        [SerializeField] private Vector3 _switchHitSize;
        [SerializeField] private Vector3 _switchHitOffset;
        [SerializeField] private List<AnimatorController> _saveAnimators;
        private Animator _animator;
        [SerializeField] private NoramlHead _head;
        [SerializeField] private NoramlHead _currentHead;
        [SerializeField] public Vector3 _headsPos;



        private void Update()
        {
            if (_player.canUseSkill1&&_currentHead!=null)
            {
                _animator.runtimeAnimatorController = _saveAnimators[0];
                Destroy(_currentHead.gameObject);
                _currentHead = null;
            }
        }

        protected override void Awake()
        {
            base.Awake();
            _animator = GetComponent<Animator>();
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
                        ihp.Damage(_player.gameObject, _player.AttackForce, out float damage);
                    }
                }
            }
        }

        protected override void Skill(int skillID)
        {
            base.Skill(skillID);

            switch(skillID)
            {
                case 1001:
                    {
                        if (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
                        {
                            _animator.runtimeAnimatorController = _saveAnimators[1];
                            return;
                        }
                        _currentHead = Instantiate(_head,
                                transform.position + new Vector3(_movement.direction * 0.1f, 0.5f, 0.0f),
                                Quaternion.Euler(new Vector3(0, _movement.direction == 1 ? 180 : 0, 90)));
                        _currentHead.SetUp(gameObject, new Vector2(_movement.direction * 2, 0), 10, _enemyMask);
                    }
                    break;
                case 1002:
                    {
                        _player.canUseSkill1 = true;
                        _player.skill1CoolTime = 0.0f;
                        _animator.runtimeAnimatorController = _saveAnimators[0];
                        _player.transform.position = _headsPos;
                        Destroy(_currentHead.gameObject);
                        _currentHead = null;
                    }
                    break;
            }

        }

        protected override void Attack_Hit()
        {
            base.Attack_Hit();
            
            _lastEnemies=
                Physics2D.OverlapBoxAll((Vector2)_player.transform.position + new Vector2(_hitOffset.x * _movement.direction,

                                                                            _hitOffset.y),
                                 _hitSize,
                                 0.0f,
                                 _enemyMask);

           
            if (_lastEnemies.Length>0)
            {
                for(int i=0;i<_lastEnemies.Length;i++)
                {
                    if (_lastEnemies[i].TryGetComponent(out IHp ihp))
                    { 
                        ihp.Damage(_player.gameObject, _player.AttackForce,out float damage);
                    }
                }
            }

        }



        private void OnDrawGizmos()
        {
            
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(_player.transform.position+new Vector3(_hitOffset.x*_movement.direction,_hitOffset.y), _hitSize);
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireCube(_player.transform.position + new Vector3(_jumpHitOffset.x * _movement.direction, _jumpHitOffset.y), _jumpHitSize);
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(_player.transform.position + _switchHitOffset, _switchHitSize);
        }
    }
}