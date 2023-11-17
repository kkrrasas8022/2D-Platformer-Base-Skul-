
using Skul.Character.PC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Skul.Data
{
    [CreateAssetMenu(fileName = "HeadItemData", menuName = "Skul/Item/Head")]
    public class HeadItemData : ItemData
    {
        public int skillCount;
        public SkulData skulData;

        public override void HadAbility(Player player)
        {
            base.HadAbility(player);
            switch (skulData.skulType)
            {
                case SkulType.Balance:
                    player.SkillCoolDown += 0.6f;
                    break;
                case SkulType.Power:
                    player.TakenDamage -= 0.2f;
                    player.maxDashCount -= 1;
                    break;
                case SkulType.Speed:
                    player.AttackSpeed += 0.2f;
                    player.MoveSpeed += 0.2f;
                    break;
            }
        }

        public override void DeleteAbility(Player player)
        {
            base.DeleteAbility(player);
            switch (skulData.skulType)
            {
                case SkulType.Balance:
                    player.SkillCoolDown -= 0.6f;
                    break;
                case SkulType.Power:
                    player.TakenDamage += 0.2f;
                    player.maxDashCount += 1;
                    break;
                case SkulType.Speed:
                    player.AttackSpeed -= 0.2f;
                    player.MoveSpeed -= 0.2f;
                    break;
            }
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
}
