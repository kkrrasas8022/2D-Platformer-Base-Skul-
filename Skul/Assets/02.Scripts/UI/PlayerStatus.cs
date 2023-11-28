using Skul.Character.PC;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Skul.Tools;

namespace Skul.UI
{
    public class PlayerStatus : SingletonUIBase<PlayerStatus>
    {
        [SerializeField] private Player _player;

        [Header("Status")]
        [SerializeField] private TMP_Text _hpTex;
        [SerializeField] private TMP_Text _takenDamageTex;
        [SerializeField] private TMP_Text _physicalTex;
        [SerializeField] private TMP_Text _magicalTex;
        [SerializeField] private TMP_Text _attackSpeedTex;
        [SerializeField] private TMP_Text _moveSpeedTex;
        [SerializeField] private TMP_Text _consentSpeedTex;
        [SerializeField] private TMP_Text _skillCoolDownTex;
        [SerializeField] private TMP_Text _switchCoolDownTex;
        [SerializeField] private TMP_Text _essenceCoolDownTex;
        [SerializeField] private TMP_Text _CriticalPerTex;
        [SerializeField] private TMP_Text _CriticalDamageTex;

        [Header("Engrave")]
        [SerializeField] private EngraveNotice _engraveNotice;
        [SerializeField] private GameObject _engraveParent;

        protected override void Awake()
        {
            base.Awake();
            _player = GameObject.FindWithTag("Player").GetComponent<Player>();
        }


        private void OnEnable()
        {
            if (_player.inventory.HaveEngrave.Count > 0)
            {
                int i = 0;
                foreach (var item in _player.inventory.HaveEngrave)
                {
                    EngraveNotice notice = Instantiate(_engraveNotice, _engraveParent.transform);
                    notice.gameObject.SetActive(false);
                    notice.transform.localPosition = new Vector3(0+235*(i/9), 270 - 60 * (i%9), 0);
                    notice.data = item.Key;
                    notice.engraveCount = item.Value;
                    i++;
                    notice.gameObject.SetActive(true);
                }
            }

            _hpTex.text = _player.hpMax.ToString();
            _takenDamageTex.text = "X "+(_player.TakenDamage).ToString("F2"); 
            _physicalTex.text=(_player.PhysicPower*100).ToString()+" %";
            _magicalTex.text= (_player.MagicPower * 100).ToString() + " %";
            _attackSpeedTex.text = (_player.AttackSpeed * 100).ToString() + " %";
            _moveSpeedTex.text= (_player.MoveSpeed * 100).ToString() + " %";
            _consentSpeedTex.text= (_player.ConsentSpeed * 100).ToString() + " %";
            _skillCoolDownTex.text = (_player.SkillCoolDown * 100).ToString() + " %";
            _switchCoolDownTex.text= (_player.SwitchCoolDown * 100).ToString() + " %";
            _essenceCoolDownTex.text = (_player.EssenceCoolDown * 100).ToString() + " %";
            _CriticalPerTex.text = (_player.CriticalPersent * 100).ToString() + " %";
            _CriticalDamageTex.text= "X " + (_player.CriticalDamage).ToString("F2");

        }
    }
}