using Skul.Character.PC;
using Skul.Item;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Skul.Data
{
    public enum ItemRate
    {
        Normal,
        Rare,
        Unique,
        Legend
    }
    public abstract class ItemData : ScriptableObject
    {
        public int id;
        public string Name;
        public Sprite Icon;
        public ItemType type;
        public ItemRate rate;
        public string description;
        public string abilityDescription;

        [Header("PowerUP")]
        public Power power;

        public virtual void HadAbility(Player player)
        {
            switch (power.type)
            {
                case StatusType.MaxHp:
                    player.hpMax += power.power;
                    break;
                case StatusType.TakenDamage:
                    player.TakenDamage -= power.power;
                    break;
                case StatusType.Physical:
                    player.PhysicPower += power.power;
                    break;
                case StatusType.Magical:
                    player.MagicPower+= power.power; 
                    break;
                case StatusType.AttackSpeed:
                    player.AttackSpeed += power.power;
                    break;
                case StatusType.MoveSpeed:
                    player.MoveSpeed += power.power;
                    break;
                case StatusType.ConsentSpeed:
                    player.ConsentSpeed += power.power;
                    break;
                case StatusType.SkillCoolDown:
                    player.SkillCoolDown += power.power;
                    break;
                case StatusType.SwitchCoolDown:
                    player.SwitchCoolDown += power.power;
                    break;
                case StatusType.EssenceCoolDown:
                    player.EssenceCoolDown += power.power;
                    break;
                case StatusType.CriticalPersent:
                    player.CriticalPersent += power.power;
                    break;
                case StatusType.CriticalDamage:
                    player.CriticalDamage += power.power;   
                    break;
            }
        }

        public virtual void DeleteAbility(Player player)
        {
            switch (power.type)
            {
                case StatusType.MaxHp:
                    player.hpMax -= power.power;
                    break;
                case StatusType.TakenDamage:
                    player.TakenDamage += power.power;
                    break;
                case StatusType.Physical:
                    player.PhysicPower -= power.power;
                    break;
                case StatusType.Magical:
                    player.MagicPower -= power.power;
                    break;
                case StatusType.AttackSpeed:
                    player.AttackSpeed -= power.power;
                    break;
                case StatusType.MoveSpeed:
                    player.MoveSpeed -= power.power;
                    break;
                case StatusType.ConsentSpeed:
                    player.ConsentSpeed -= power.power;
                    break;
                case StatusType.SkillCoolDown:
                    player.SkillCoolDown -= power.power;
                    break;
                case StatusType.SwitchCoolDown:
                    player.SwitchCoolDown -= power.power;
                    break;
                case StatusType.EssenceCoolDown:
                    player.EssenceCoolDown -= power.power;
                    break;
                case StatusType.CriticalPersent:
                    player.CriticalPersent -= power.power;
                    break;
                case StatusType.CriticalDamage:
                    player.CriticalDamage -= power.power;
                    break;
            }
        }
        public virtual void HitAbility()
        {
            
        }

        public virtual void UseSkillAbility()
        {

        }
    }
}
