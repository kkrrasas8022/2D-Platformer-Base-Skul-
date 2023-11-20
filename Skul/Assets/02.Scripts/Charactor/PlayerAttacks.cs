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
        public void InitAttackRenderer()
        {
            _player = GetComponentInParent<Player>();
            _movement=GetComponentInParent<PlayerMovement>();
        }

         

        private void Awake()
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