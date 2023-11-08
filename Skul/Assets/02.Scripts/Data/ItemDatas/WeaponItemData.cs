using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Skul.Data
{
    [CreateAssetMenu(fileName = "WeaponItemData", menuName = "Skul/Item/Weapon")]
    public class WeaponItemData : ItemData
    {
        public Engrave[] engraves = new Engrave[2];

        public enum PowerType
        {
            Physical,
            Magical,
            AttackSpeed,
            MoveSpeed,
            ConsentSpeed,
            SkillCoolDown,
            SwitchCoolDown,
            EssenceCoolDown,
            CriticalPersent,
            CriticalDamage,
        }
        [Serializable]
        public class Power
        {
            public PowerType powerType;
            public float power;
        }
        [SerializeField]public Power power;
    }
}