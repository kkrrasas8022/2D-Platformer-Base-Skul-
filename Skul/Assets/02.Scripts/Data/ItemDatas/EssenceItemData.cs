using Skul.Character.PC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Skul.Data
{
    [CreateAssetMenu(fileName = "EssenceItemData", menuName = "Skul/Item/Essence")]
    public class EssenceItemData : ItemData
    {
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

        public void EssenceSkill()
        {

        }
    }
}
