
using Skul.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Skul.Data
{
    public class SkillManager : SingletonMonoBase<SkillManager>
    {
        public Dictionary<int, SkillData> skillDatum;
        public SkillData this[int id] => skillDatum[id];

        [SerializeField] private List<ActiveSkillData> _activeSkillDatum;
        [SerializeField] private List<SwitchSkillData> _switchSkillDatum;



        protected override void Awake()
        {
            base.Awake();
            skillDatum = new Dictionary<int, SkillData>();

            foreach (var item in _activeSkillDatum)
            {
                skillDatum.Add(item.id, item);
            }



            foreach (var item in _switchSkillDatum)
            {
                skillDatum.Add(item.id, item);
            }


        }
    }
}