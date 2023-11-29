using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Skul.Character.PC;
using Skul.Character;
using Skul.Data;
using Skul.Tools;
using Skul.GameElement;
using System.Runtime.CompilerServices;
using System;

namespace Skul.UI
{
    public class MainUI : SingletonUIBase<MainUI>
    {
        [SerializeField] private Slider _hpBar;
        [SerializeField] private TMP_Text _hpText;
        [SerializeField] private TMP_Text _hpMaxText;
        [SerializeField] private Image _mainFace;
        [SerializeField] private Image _subFace;
        [SerializeField] private Image _switchFill;
        [SerializeField] private Image _skill1;
        [SerializeField] private Image _skill1Fill;
        [SerializeField] private Image _skill2;
        [SerializeField] private Image _skill2Fill;

        [SerializeField] private TMP_Text _curCoin;
        [SerializeField] private TMP_Text _curBone;

        [SerializeField] private List<GameObject> buffList;

        public Action<Buff> StartBuff;
        public Action<Buff> EndBuff;

        protected override void Awake()
        {
            base.Awake();
            buffList = new List<GameObject>();
            StartBuff += (buff) =>
            {
                Power power = buff.data.power;
                switch (power.type)
                {
                    case StatusType.MaxHp:
                        GameManager.instance.player.hpMax += power.power;
                        break;
                    case StatusType.TakenDamage:
                        GameManager.instance.player.TakenDamage -= power.power;
                        break;
                    case StatusType.Physical:
                        GameManager.instance.player.PhysicPower += power.power;
                        break;
                    case StatusType.Magical:
                        GameManager.instance.player.MagicPower += power.power;
                        break;
                    case StatusType.AttackSpeed:
                        GameManager.instance.player.AttackSpeed += power.power;
                        break;
                    case StatusType.MoveSpeed:
                        GameManager.instance.player.MoveSpeed += power.power;
                        break;
                    case StatusType.ConsentSpeed:
                        GameManager.instance.player.ConsentSpeed += power.power;
                        break;
                    case StatusType.SkillCoolDown:
                        GameManager.instance.player.SkillCoolDown += power.power;
                        break;
                    case StatusType.SwitchCoolDown:
                        GameManager.instance.player.SwitchCoolDown += power.power;
                        break;
                    case StatusType.EssenceCoolDown:
                        GameManager.instance.player.EssenceCoolDown += power.power;
                        break;
                    case StatusType.CriticalPersent:
                        GameManager.instance.player.CriticalPersent += power.power;
                        break;
                    case StatusType.CriticalDamage:
                        GameManager.instance.player.CriticalDamage += power.power;
                        break;
                }
                buffList.Add(buff.gameObject);
                int i = 0;
                foreach(var item in buffList)
                {
                    item.transform.localPosition = new Vector3(-100.0f + 100 * i, -300, 0);
                    i++;
                }
            };

            EndBuff += (buff) =>
            {
                Power power = buff.data.power;
                switch (power.type)
                {
                    case StatusType.MaxHp:
                        GameManager.instance.player.hpMax -= power.power;
                        break;
                    case StatusType.TakenDamage:
                        GameManager.instance.player.TakenDamage += power.power;
                        break;
                    case StatusType.Physical:
                        GameManager.instance.player.PhysicPower -= power.power;
                        break;
                    case StatusType.Magical:
                        GameManager.instance.player.MagicPower -= power.power;
                        break;
                    case StatusType.AttackSpeed:
                        GameManager.instance.player.AttackSpeed -= power.power;
                        break;
                    case StatusType.MoveSpeed:
                        GameManager.instance.player.MoveSpeed -= power.power;
                        break;
                    case StatusType.ConsentSpeed:
                        GameManager.instance.player.ConsentSpeed -= power.power;
                        break;
                    case StatusType.SkillCoolDown:
                        GameManager.instance.player.SkillCoolDown -= power.power;
                        break;
                    case StatusType.SwitchCoolDown:
                        GameManager.instance.player.SwitchCoolDown -= power.power;
                        break;
                    case StatusType.EssenceCoolDown:
                        GameManager.instance.player.EssenceCoolDown -= power.power;
                        break;
                    case StatusType.CriticalPersent:
                            GameManager.instance.player.CriticalPersent -= power.power;
                        break;
                    case StatusType.CriticalDamage:
                        GameManager.instance.player.CriticalDamage -= power.power;
                        break;
                }
                buffList.Remove(buff.gameObject);
                int i = 0;
                foreach (var item in buffList)
                {
                    item.transform.localPosition = new Vector3(-100.0f + 100 * i, -300, 0);
                    i++;
                }
            };

        }

        private void Update()
        {
            _skill1Fill.fillAmount = GameManager.instance.player.skill1CoolTime/((ActiveSkillData)SkillManager.instance[GameManager.instance.player.currentRen.hadSkillsID[0]]).CoolTime;
            if(GameManager.instance.player.currentRen.hadSkillsID.Count>1)
                _skill2Fill.fillAmount = GameManager.instance.player.skill2CoolTime/((ActiveSkillData)SkillManager.instance[GameManager.instance.player.currentRen.hadSkillsID[1]]).CoolTime;
            _switchFill.fillAmount = GameManager.instance.player.switchCoolTime / GameManager.instance.player.switchMaxCooltime;

        }

        private void Start()
        {
            _mainFace.sprite = GameManager.instance.player.inventory.CurHeadData.Icon;
            if (GameManager.instance.player.inventory.SaveHeadData == null)
                _subFace.color = Color.clear;
            else
            {
                _subFace.color = Color.white;
                _subFace.sprite = GameManager.instance.player.inventory.SaveHeadData.Icon; 
            }
            _skill1.sprite = SkillManager.instance[GameManager.instance.player.currentRen.hadSkillsID[0]].Icon;
            _skill2.sprite = SkillManager.instance[GameManager.instance.player.currentRen.hadSkillsID[1]].Icon;
            _subFace=_subFace.GetComponent<Image>();
            _hpBar.minValue = 0.0f;
            _hpBar.maxValue = GameManager.instance.player.hpMax;
            _hpBar.value = GameManager.instance.player.hp;
            _hpText.text = ((int)GameManager.instance.player.hp).ToString();
            _hpMaxText.text = ((int)GameManager.instance.player.hpMax).ToString();

            _curCoin.text = GameManager.instance.player.curCoin.ToString();
            _curBone.text = GameManager.instance.player.curBone.ToString();

           GameManager.instance.player.OnCoinChanged += (value) =>
            {
                _curCoin.text = value.ToString();
            };
           GameManager.instance.player.OnBoneChanged += (value) =>
            {
                _curBone.text = value.ToString();
            };

            GameManager.instance.player.onHpChanged += (hp) =>
            {
                _hpBar.value = hp;
                _hpText.text = ((int)hp).ToString();
            };
           GameManager.instance.player.onHpMaxChanged += (maxhp) =>
            {
                _hpBar.maxValue = maxhp;
                _hpBar.value = GameElement.GameManager.instance.player.hp;
                _hpMaxText.text= maxhp.ToString();
            };

            GameManager.instance.player.OnSwitch += () =>
            {
                _subFace.sprite = GameManager.instance.player.inventory.CurHeadData.Icon;
                _mainFace.sprite = GameManager.instance.player.inventory.SaveHeadData.Icon;
                _skill1.sprite = SkillManager.instance[(GameManager.instance.player.currentRen 
                    == GameManager.instance.player.renderers[0] ? GameManager.instance.player.renderers[1] : GameManager.instance.player.renderers[0]).hadSkillsID[0]].Icon;

                if (GameManager.instance.player.inventory.SaveHeadData.skillCount > 1)
                { 
                    _skill2.sprite = SkillManager.instance[(GameManager.instance.player.currentRen
                        == GameManager.instance.player.renderers[0] ? GameManager.instance.player.renderers[1] : GameManager.instance.player.renderers[0]).hadSkillsID[1]].Icon;
                    _skill2.color = Color.white;
                }
                else
                {
                    _skill2.sprite = null;
                    _skill2.color = Color.clear;
                }
            };

            GameManager.instance.player.inventory.OnHeadAdd += (data) =>
            {
                _subFace.color = Color.white;
                _subFace.sprite = GameManager.instance.player.inventory.SaveHeadData.Icon;
                _mainFace.sprite = GameManager.instance.player.inventory.CurHeadData.Icon;
                _skill1.sprite = SkillManager.instance[GameManager.instance.player.currentRen.hadSkillsID[0]].Icon;
                if (GameManager.instance.player.inventory.CurHeadData.skillCount > 1)
                    _skill2.sprite = SkillManager.instance[GameManager.instance.player.currentRen.hadSkillsID[1]].Icon;
                else
                {
                    _skill2.sprite = null;
                    _skill2.color = Color.clear;
                }
            };
        }
        //private void Update()
        //{
        //    _subFace.sprite = _player.saveData.SkulFace; 
        //    _mainFace.sprite = _player.currentData.SkulFace;
        //}

    }
}
