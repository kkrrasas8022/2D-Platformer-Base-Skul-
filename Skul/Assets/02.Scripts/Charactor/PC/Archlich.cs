using Skul.FSM.States;
using Skul.FSM;
using System;
using System.Collections.Generic;
using UnityEngine;
using Skul.Movement;
using UnityEditor.Animations;
using Skul.Data;
using Skul.Character.Enemy;
using UnityEditor.PackageManager;
using Skul.UI;

namespace Skul.Character.PC
{
    public class Archlich:PlayerAttacks
    { 
        [SerializeField] private Collider2D[] _lastEnemies;

        [SerializeField] private Vector3 _hitPos;

        [SerializeField] private Vector3 _jumpHitPos;

        [SerializeField] private Chains _chains;

        [SerializeField] private PlayerProjectile _attackChain;
        [SerializeField] private PlayerProjectile _nowAttackChain;

        [SerializeField] private Vector3[] skill1Pos=new Vector3[3]; 
        [SerializeField] private Vector3[] skill2Pos=new Vector3[5];

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
            Instantiate(_attackChain, _player.transform.position + _jumpHitPos, Quaternion.Euler(new Vector3(0,0,-25)))
                .SetUp(gameObject, Vector2.zero, _player.AttackForce, _enemyMask);

        }
        protected override void Skill(int skillID)
        {
            base.Skill(skillID);

            switch(skillID)
            {
                case 1014:
                    {
                        for(int i=0;i<skill1Pos.Length;i++)
                        {
                            Instantiate(_attackChain, _player.transform.position + new Vector3(skill1Pos[i].x*_movement.direction, skill1Pos[i].y), 
                                Quaternion.Euler(new Vector3(0,_movement.direction==1?0:180,-30+(30*i))))
                                .SetUp(gameObject, Vector2.zero, _player.AttackForce, _enemyMask);
                        }
                    }
                    break;
                case 1015:
                    {
                        for (int i = 0; i < skill2Pos.Length; i++)
                        {
                            PlayerProjectile a = Instantiate(_attackChain, _player.transform.position + new Vector3(skill2Pos[i].x * _movement.direction, skill2Pos[i].y),
                                Quaternion.Euler(new Vector3(0, 0, 90)));
                            a.SetUp(gameObject, Vector2.zero, _player.AttackForce, _enemyMask);
                            a.transform.localScale = new Vector2(0.5f, 0.5f);
                        }
                    }
                    break;
                case 1016:
                    {
                        Chains c = Instantiate(_chains, _player.transform.position, Quaternion.identity);
                        for(int i=0;i<_chains.cains.Length;i++)
                            c.cains[i].SetUp(gameObject,Vector2.zero, _player.AttackForce, _enemyMask);
                    }
                    break;

            }

        }

        

        protected override void Attack_Hit()
        {
            base.Attack_Hit();
            Instantiate(_attackChain, _player.transform.position+_hitPos, Quaternion.identity)
                .SetUp(gameObject,Vector2.zero,_player.AttackForce,_enemyMask);
        }



        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(_player.transform.position, _player.transform.position+_hitPos);

            Gizmos.color = Color.blue;
            for(int i = 0; i < skill1Pos.Length;i++)
            {
                Gizmos.DrawLine(_player.transform.position, _player.transform.position+ new Vector3(skill1Pos[i].x * _movement.direction, skill1Pos[i].y));
            }

            Gizmos.color = Color.red;
            for (int i = 0; i < skill2Pos.Length; i++)
            {
                Gizmos.DrawLine(_player.transform.position+new Vector3(0,0.5f), _player.transform.position+ new Vector3(skill2Pos[i].x * _movement.direction, skill2Pos[i].y));
            }

            Gizmos.color = Color.magenta;
            Gizmos.DrawLine(_player.transform.position + new Vector3(0, 0.5f), _player.transform.position + _jumpHitPos);


        }
    }
}