using Skul.Character.PC;
using Skul.Data;
using Skul.Movement;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Skul.Character
{
    public abstract class PlayerAttacks:MonoBehaviour
    {
        [SerializeField]protected Player _player;
        [SerializeField]protected PlayerMovement _movement;
        [SerializeField] protected LayerMask _enemyMask;
        [SerializeField] public List<int> hadSkillsID;

        [SerializeField] private SkulData _data;

        public void InitAttackRenderer()
        {
            _player = GetComponentInParent<Player>();
            _movement=GetComponentInParent<PlayerMovement>();
            _player.PhysicPower += _data.physicPower;
            _player.MagicPower += _data.magicPower;
            _player.TakenDamage -= _data.takenDamage;
            _player.AttackSpeed += _data.attackSpeed;
            _player.MoveSpeed += _data.moveSpeed;
            _player.ConsentSpeed += _data.consentSpeed;
            _player.SkillCoolDown += _data.skillCoolDown;
            _player.EssenceCoolDown += _data.essenceCoolDown;
            _player.SwitchCoolDown += _data.switchCoolDown;
            _player.CriticalPersent += _data.critical;
            _player.CriticalDamage += _data.critDmg;
        }

        protected virtual void Awake()
        { }

        protected virtual void Start()
        {
            InitAttackRenderer();
        }

        protected virtual void SwitchAttack()
        {
            Debug.Log("SwitchAttack");
        }
        protected virtual void JumpAttack()
        {
            Debug.Log("JumpAttack");
        }
        protected virtual void Skill_1()
        {
            Debug.Log("Skill_1");
        }

        protected virtual void Skill_2()
        {
            Debug.Log("Skill_2");
        }

        protected virtual void Attack_Hit()
        {
            Debug.Log("Hit");
        }
    }
}