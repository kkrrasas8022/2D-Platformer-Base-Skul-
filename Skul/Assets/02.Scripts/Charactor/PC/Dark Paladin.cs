using Skul.FSM.States;
using Skul.FSM;
using System;
using System.Collections.Generic;
using UnityEngine;
using Skul.Movement;
using UnityEditor.Animations;
using Skul.Data;
using Skul.Character.Enemy;
using UnityEngine.UIElements;

namespace Skul.Character.PC
{
    public class DarkPaladin:PlayerAttacks
    { 
        [SerializeField] private Collider2D _lastEnemy;
        [SerializeField] private Collider2D[] _lastEnemies;

        [SerializeField] private Vector3 _hitSize;
        [SerializeField] private Vector3 _hitOffset;

        [SerializeField] private PlayerProjectile _cross;
        [SerializeField] private PlayerProjectile _wave;

        [SerializeField] private float _waveSpeed;

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
            Instantiate(_cross,transform.position+new Vector3(0,2),Quaternion.identity).
                SetUp(gameObject, new Vector2(0.0f,0.0f), _player.AttackForce, _enemyMask);
        }

        protected override void JumpAttack()
        {
            base.JumpAttack();
            _lastEnemies =
                Physics2D.OverlapBoxAll((Vector2)_player.transform.position + new Vector2(_hitOffset.x * _movement.direction,

                                                                            _hitOffset.y),
                                 _hitSize,
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
                case 1010:
                    {
                        Attack_Hit();
                    }
                    break;
                case 1011:
                    {
                        Attack_Hit();
                    }
                    break;
                case 1012:
                    {
                        PlayerProjectile nowWave = Instantiate(_wave, transform.position + new Vector3(0, 1.0f), Quaternion.Euler(new Vector3(0, _movement.direction == 1 ? 0 : 180)));
                        nowWave.SetUp(gameObject, new Vector2((_movement.direction == 1 ? 1.0f : -1.0f) * _waveSpeed, 0.0f), _player.AttackForce, _enemyMask);
                        
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
                        float a;
                        ihp.Damage(_player.gameObject, _player.AttackForce,out a);
                    }
                }
            }

        }



        private void OnDrawGizmos()
        {
            
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(_player.transform.position+new Vector3(_hitOffset.x*_movement.direction,_hitOffset.y), _hitSize);

    
        }
    }
}