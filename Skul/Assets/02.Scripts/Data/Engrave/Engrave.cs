using Skul.Character.PC;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Skul.Data
{
    [CreateAssetMenu(fileName = ("Engraves"), menuName = ("Skul/Engrave"))]
    public class Engrave : ScriptableObject
    {
        public string Name;
        public string synergyAbility;
        public List<int> synergyCount;
        public List<Power> synergyPower;
        public Sprite Icon;

        public void ActivationSynergy(Player player,int nowSynergyCount)
        {
            for(int i=0;i<synergyCount.Count;i++)
            {
                if (nowSynergyCount >= synergyCount[i])
                {
                    switch (synergyPower[i].type)
                    {
                        case StatusType.MaxHp:
                            player.hpMax += synergyPower[i].power;
                            break;
                        case StatusType.TakenDamage:
                            player.TakenDamage-= synergyPower[i].power;
                            break;
                        case StatusType.Physical:
                            player.PhysicPower += synergyPower[i].power;
                            break;
                        case StatusType.Magical:
                            player.MagicPower += synergyPower[i].power;
                            break;
                        case StatusType.AttackSpeed:
                            player.AttackSpeed += synergyPower[i].power;
                            break;
                        case StatusType.MoveSpeed:
                            player.AttackSpeed += synergyPower[i].power;
                            break;
                        case StatusType.ConsentSpeed:
                            player.AttackSpeed += synergyPower[i].power;
                            break;
                        case StatusType.SkillCoolDown:
                            player.SkillCoolDown += synergyPower[i].power;
                            break;
                        case StatusType.SwitchCoolDown:
                            player.SwitchCoolDown+= synergyPower[i].power;
                            break;
                        case StatusType.EssenceCoolDown:
                            player.EssenceCoolDown += synergyPower[i].power;
                            break;
                        case StatusType.CriticalPersent:
                            player.CriticalPersent += synergyPower[i].power;
                            break;
                        case StatusType.CriticalDamage:
                            player.CriticalPersent += synergyPower[i].power;
                            break;
                    }
                }
            }
        }
    }
}