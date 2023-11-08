using Skul.Character.PC;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

namespace Skul.Data
{
    public abstract class SkulData : ScriptableObject
    {
        public int id;
        public GameObject Renderer;
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