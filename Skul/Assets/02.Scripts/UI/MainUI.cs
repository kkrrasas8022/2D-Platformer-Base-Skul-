using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Skul.Character.PC;
using Skul.Character;
using Skul.Data;

namespace Skul.UI
{
    public class MainUI : MonoBehaviour
    {
        [SerializeField] private Slider _hpBar;
        [SerializeField] private TMP_Text _hpText;
        [SerializeField] private TMP_Text _hpMaxText;
        [SerializeField] private Image _mainFace;
        [SerializeField] private Image _subFace;
        [SerializeField] private Image _skill1;
        [SerializeField] private Image _skill1Fill;
        [SerializeField] private Image _skill2;
        [SerializeField] private Image _skill2Fill;
        [SerializeField] private Player _player;

        [SerializeField] private TMP_Text _curCoin;
        [SerializeField] private TMP_Text _curBone;

        private void Update()
        {
            _skill1Fill.fillAmount = _player.skill1CoolTime/((ActiveSkillData)SkillManager.instance[_player.currentRen.hadSkillsID[0]]).CoolTime;
            if(_player.currentRen.hadSkillsID.Count>1)
                _skill2Fill.fillAmount = _player.skill2CoolTime/((ActiveSkillData)SkillManager.instance[_player.currentRen.hadSkillsID[1]]).CoolTime;
        }

        private void Start()
        {
            _mainFace.sprite = _player.inventory.CurHeadData.skulData.SkulFace;
            if (_player.inventory.SaveHeadData == null)
                _subFace.color = Color.clear;
            else
            {
                _subFace.color = Color.white;
                _subFace.sprite = _player.inventory.SaveHeadData.skulData.SkulFace; 
            }
            _skill1.sprite = SkillManager.instance[_player.currentRen.hadSkillsID[0]].Icon;
            _skill2.sprite = SkillManager.instance[_player.currentRen.hadSkillsID[1]].Icon;
            _subFace=_subFace.GetComponent<Image>();
            _hpBar.minValue = 0.0f;
            _hpBar.maxValue = _player.hpMax;
            _hpBar.value = _player.hp;
            _hpText.text = ((int)_player.hp).ToString();
            _hpMaxText.text = ((int)_player.hpMax).ToString();

            _curCoin.text=_player.curCoin.ToString();
            _curBone.text=_player.curBone.ToString();

            _player.OnCoinChanged += (value) =>
            {
                _curCoin.text = value.ToString();
            };
            _player.OnBoneChanged += (value) =>
            {
                _curBone.text = value.ToString();
            };

            _player.onHpChanged += (hp) =>
            {
                _hpBar.value = hp;
                _hpText.text = ((int)hp).ToString();
            };
            _player.onHpMaxChanged += (maxhp) =>
            {
                
                _hpBar.maxValue = maxhp;
                _hpBar.value = _player.hp;
                _hpMaxText.text= maxhp.ToString();
            };
            _player.OnSwitch += () =>
            {
                _subFace.sprite = _player.inventory.CurHeadData.skulData.SkulFace;
                _mainFace.sprite = _player.inventory.SaveHeadData.skulData.SkulFace;
                _skill1.sprite = SkillManager.instance[(_player.currentRen == _player._renderers[0] ? _player._renderers[1] : _player._renderers[0]).hadSkillsID[0]].Icon;
                if (_player.inventory.CurHeadData.skillCount > 1)
                    _skill2.sprite = SkillManager.instance[(_player.currentRen == _player._renderers[0] ? _player._renderers[1] : _player._renderers[0]).hadSkillsID[1]].Icon;
            };
            _player.inventory.OnHeadAdd += (data) =>
            {
                _subFace.color = Color.white;
                _subFace.sprite = _player.inventory.SaveHeadData.skulData.SkulFace;
                _mainFace.sprite = _player.inventory.CurHeadData.skulData.SkulFace;
                _skill1.sprite = SkillManager.instance[_player.currentRen.hadSkillsID[0]].Icon;
                if (_player.inventory.CurHeadData.skillCount > 1)
                    _skill2.sprite = SkillManager.instance[_player.currentRen.hadSkillsID[1]].Icon;
            };
        }
        //private void Update()
        //{
        //    _subFace.sprite = _player.saveData.SkulFace; 
        //    _mainFace.sprite = _player.currentData.SkulFace;
        //}

    }
}
