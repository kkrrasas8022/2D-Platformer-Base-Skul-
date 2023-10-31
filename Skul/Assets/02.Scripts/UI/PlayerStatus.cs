using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Skul.Character.PC;
using Skul.Character;

public class PlayerStatus : MonoBehaviour
{
    [SerializeField] private Slider _hpBar;
    [SerializeField] private TMP_Text _hpText;
    [SerializeField] private TMP_Text _hpMaxText;
    [SerializeField] private Image _mainFace;
    [SerializeField] private Image _subFace;
    [SerializeField] private Player _player;
    private void Start()
    {
        //_mainFace=_mainFace.GetComponent<Image>();
        //_subFace=_subFace.GetComponent<Image>();
        _hpBar.minValue = 0.0f;
        _hpBar.maxValue = _player.hpMax;
        _hpBar.value = _player.hp;
        _hpText.text = ((int)_player.hp).ToString();
        _hpMaxText.text = ((int)_player.hpMax).ToString();
        _player.onHpChanged += (value) =>
        {
            _hpBar.value = value;
            _hpText.text = ((int)value).ToString();
        };
    }
   private void Update()
   {
       _subFace.sprite = _player.saveData.SkulFace;
       _mainFace.sprite = _player.currentData.SkulFace;
   }

}
