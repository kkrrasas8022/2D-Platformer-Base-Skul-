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
        public Power power;
        public int hitPer;
        public float skillabilityPower;
        public int skillPer;
        public float hitabilityPower;

        public void HitAbility()
        {
            if(hitPer<UnityEngine.Random.Range(0, 100))
            {

            }
        }

        public void UseSkillAbility()
        {

        }


    }
    [Serializable]
    public class Power
    {
        public StatusType type;
        public float power;
    }
}