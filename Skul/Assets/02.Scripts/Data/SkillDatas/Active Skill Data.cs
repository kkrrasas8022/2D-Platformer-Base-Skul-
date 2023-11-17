using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Skul.Data
{
    [CreateAssetMenu(fileName = "ActiveSkillData", menuName = "Skul/Skill/Active")]
    public class ActiveSkillData : SkillData
    {
        public float CoolTime;
    }
}