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
        public string Description;
        public PlayerAttacks Renderer;
        public Sprite SkulFace;
        public List<SkillData> activeSkills;
        public SkillData switchSkill;
        public SkillData passiveSkill;
        public SkillData specialSkill;
        public AttackType attackType;

        [Header("Status")]
        public float takenDamage;
        public float physicPower;
        public float magicPower;
        public float attackSpeed;
        public float moveSpeed;
        public float consentSpeed;
        public float skillCoolDown;
    }
}