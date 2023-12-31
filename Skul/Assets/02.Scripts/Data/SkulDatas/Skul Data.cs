using Skul.Character.PC;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using Skul.Character;

namespace Skul.Data
{
    public enum SkulType
    {
        Balance,
        Power,
        Speed,
    }
    public abstract class SkulData : ScriptableObject
    {
        public int id;
        public string Name;
        public SkulType skulType;
        public PlayerAttacks Renderer;
        public List<ActiveSkillData> activeSkills;
        public SwitchSkillData switchSkill;
        public AttackType attackType;
        public int attackComboCount;

        [Header("Status")]
        public float takenDamage;
        public float physicPower;
        public float magicPower;
        public float attackSpeed;
        public float moveSpeed;
        public float consentSpeed;
        public float skillCoolDown;
        public float essenceCoolDown;
        public float switchCoolDown;
        public float critical;
        public float critDmg;
    }
}