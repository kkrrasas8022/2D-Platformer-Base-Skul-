using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Skul.Character.PC;
using Skul.Character;

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
        [SerializeField] private Image _skill2;
        [SerializeField] private Player _player;

        [SerializeField] private TMP_Text _curCoin;
        [SerializeField] private TMP_Text _curBone;

        private void Start()
        {
            _mainFace.sprite = _player.currentHeadData.skulData.SkulFace;
            _subFace.sprite = _player.saveHeadData.skulData.SkulFace;
            _skill1.sprite = _player.currentHeadData.skulData.activeSkills[0].Icon;
            _skill2.sprite = _player.currentHeadData.skulData.activeSkills[1].Icon;
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
            _player.OnSwitch += () =>
            {
                _subFace.sprite = _player.currentHeadData.skulData.SkulFace;
                _mainFace.sprite = _player.saveHeadData.skulData.SkulFace;
                _skill1.sprite = _player.saveHeadData.skulData.activeSkills[0].Icon;
                _skill2.sprite = _player.saveHeadData.skulData.activeSkills[1].Icon;
            };
        }
        //private void Update()
        //{
        //    _subFace.sprite = _player.saveData.SkulFace; 
        //    _mainFace.sprite = _player.currentData.SkulFace;
        //}

    }
}
