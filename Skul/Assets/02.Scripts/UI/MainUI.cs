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

        protected override void Awake()
        {
            base.Awake();
        }

        private void Update()
        {
            _skill1Fill.fillAmount = GameElement.GameManager.instance.player.skill1CoolTime/((ActiveSkillData)SkillManager.instance[GameElement.GameManager.instance.player.currentRen.hadSkillsID[0]]).CoolTime;
            if(GameElement.GameManager.instance.player.currentRen.hadSkillsID.Count>1)
                _skill2Fill.fillAmount = GameElement.GameManager.instance.player.skill2CoolTime/((ActiveSkillData)SkillManager.instance[GameElement.GameManager.instance.player.currentRen.hadSkillsID[1]]).CoolTime;
            _switchFill.fillAmount = GameElement.GameManager.instance.player.switchCoolTime / GameElement.GameManager.instance.player.switchMaxCooltime;

        }

        private void Start()
        {
            _mainFace.sprite = GameElement.GameManager.instance.player.inventory.CurHeadData.skulData.SkulFace;
            if (GameElement.GameManager.instance.player.inventory.SaveHeadData == null)
                _subFace.color = Color.clear;
            else
            {
                _subFace.color = Color.white;
                _subFace.sprite = GameElement.GameManager.instance.player.inventory.SaveHeadData.skulData.SkulFace; 
            }
            _skill1.sprite = SkillManager.instance[GameElement.GameManager.instance.player.currentRen.hadSkillsID[0]].Icon;
            _skill2.sprite = SkillManager.instance[GameElement.GameManager.instance.player.currentRen.hadSkillsID[1]].Icon;
            _subFace=_subFace.GetComponent<Image>();
            _hpBar.minValue = 0.0f;
            _hpBar.maxValue = GameElement.GameManager.instance.player.hpMax;
            _hpBar.value = GameElement.GameManager.instance.player.hp;
            _hpText.text = ((int)GameElement.GameManager.instance.player.hp).ToString();
            _hpMaxText.text = ((int)GameElement.GameManager.instance.player.hpMax).ToString();

            _curCoin.text = GameElement.GameManager.instance.player.curCoin.ToString();
            _curBone.text = GameElement.GameManager.instance.player.curBone.ToString();

            GameElement.GameManager.instance.player.OnCoinChanged += (value) =>
            {
                _curCoin.text = value.ToString();
            };
            GameElement.GameManager.instance.player.OnBoneChanged += (value) =>
            {
                _curBone.text = value.ToString();
            };

            GameElement.GameManager.instance.player.onHpChanged += (hp) =>
            {
                _hpBar.value = hp;
                _hpText.text = ((int)hp).ToString();
            };
            GameElement.GameManager.instance.player.onHpMaxChanged += (maxhp) =>
            {

                _hpBar.maxValue = maxhp;
                _hpBar.value = GameElement.GameManager.instance.player.hp;
                _hpMaxText.text= maxhp.ToString();
            };
            GameElement.GameManager.instance.player.OnSwitch += () =>
            {
                _subFace.sprite = GameElement.GameManager.instance.player.inventory.CurHeadData.skulData.SkulFace;
                _mainFace.sprite = GameElement.GameManager.instance.player.inventory.SaveHeadData.skulData.SkulFace;
                _skill1.sprite = SkillManager.instance[(GameElement.GameManager.instance.player.currentRen 
                    == GameElement.GameManager.instance.player._renderers[0] ? GameElement.GameManager.instance.player._renderers[1] : GameElement.GameManager.instance.player._renderers[0]).hadSkillsID[0]].Icon;

                if (GameElement.GameManager.instance.player.inventory.CurHeadData.skillCount > 1)
                { 
                    _skill2.sprite = SkillManager.instance[(GameElement.GameManager.instance.player.currentRen
                        == GameElement.GameManager.instance.player._renderers[0] ? GameElement.GameManager.instance.player._renderers[1] : GameElement.GameManager.instance.player._renderers[0]).hadSkillsID[1]].Icon; 
                }
                else
                {
                    _skill2.sprite = null;
                    _skill2.color = Color.clear;
                }
            };
            GameElement.GameManager.instance.player.inventory.OnHeadAdd += (data) =>
            {
                _subFace.color = Color.white;
                _subFace.sprite = GameElement.GameManager.instance.player.inventory.SaveHeadData.skulData.SkulFace;
                _mainFace.sprite = GameElement.GameManager.instance.player.inventory.CurHeadData.skulData.SkulFace;
                _skill1.sprite = SkillManager.instance[GameElement.GameManager.instance.player.currentRen.hadSkillsID[0]].Icon;
                if (GameElement.GameManager.instance.player.inventory.CurHeadData.skillCount > 1)
                    _skill2.sprite = SkillManager.instance[GameElement.GameManager.instance.player.currentRen.hadSkillsID[1]].Icon;
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
