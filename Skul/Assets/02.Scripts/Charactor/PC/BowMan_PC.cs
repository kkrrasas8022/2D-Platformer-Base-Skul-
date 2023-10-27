using Skul.FSM.States;
using Skul.FSM;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Skul.Character.PC
{
    public class BowMan_PC:MonoBehaviour
    {
        private void Skill_1()
        {
            Debug.Log("Skill_1");
        }

        private void Skill_2()
        {
            Debug.Log("Skill_2");
        }

        private void Attack_Hit()
        {
            Debug.Log("Hit");
        }
    }
}