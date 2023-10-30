using Skul.FSM.States;
using Skul.FSM;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Skul.Character.PC
{
    public class BowMan_PC:PlayerAttacks
    {
        protected override void SwitchAttack()
        {
            base.SwitchAttack();
        }
        protected override void JumpAttack()
        {
            base.JumpAttack();
        }
        protected override void Attack_Hit()
        {
            base.Attack_Hit();
        }
        protected override void Skill_1()
        {
            base.Skill_1();
        }
        protected override void Skill_2()
        {
            base.Skill_2();
        }
    }
}