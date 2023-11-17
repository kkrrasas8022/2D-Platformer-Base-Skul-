using Skul.Character.PC;
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
        public int hitPer;
        public float skillabilityPower;
        public int skillPer;
        public float hitabilityPower;

        public override void HadAbility(Player player)
        {
            base.HadAbility(player);
        }
        public override void DeleteAbility(Player player)
        {
            base.DeleteAbility(player);
        }
        public override void HitAbility()
        {
            base.HitAbility();
        }

        public override void UseSkillAbility()
        {
            base.UseSkillAbility();
        }


    }
    [Serializable]
    public class Power
    {
        public StatusType type;
        public float power;
    }
}