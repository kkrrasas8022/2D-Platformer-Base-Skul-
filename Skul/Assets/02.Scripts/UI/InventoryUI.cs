using Skul.Character.PC;
using Skul.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Skul.InputSystem;
using Skul.Data;
using System;

namespace Skul.UI
{ 
    public class InventoryUI : MonoBehaviour, IUI
    {
        [SerializeField] private Player _player;
        [SerializeField] private GameObject _Skills;

        [SerializeField] private InventoryBox _curItemBox;
        public event Action OnCurChanged;
        [SerializeField] private int _boxIndex;
        [SerializeField] private InventoryBox[] ItemBoxs;

        [Header("Description")]
        [SerializeField] private Image _itemIcon;
        [SerializeField] private TMP_Text _itemName;
        [SerializeField] private TMP_Text _rateTex;
        [SerializeField] private TMP_Text _typeTex;
        [SerializeField] private TMP_Text _descriptionTex;
        [SerializeField] private TMP_Text _abilityTex;
        [SerializeField] private TMP_Text _switchSkillTex;
        [SerializeField] private TMP_Text _skill1Name;
        [SerializeField] private TMP_Text _skill2Name;
        [SerializeField] private Image _skill1Icon;
        [SerializeField] private Image _skill2Icon;

        [Header("Details")]
        [SerializeField] private Image _detailSkill1Icon;
        [SerializeField] private Image _detailSkill2Icon;
        [SerializeField] private TMP_Text _detailSkill1Name;
        [SerializeField] private TMP_Text _detailSkill2Name;
        [SerializeField] private TMP_Text _detailSkill1CoolTime;
        [SerializeField] private TMP_Text _detailSkill2CoolTime;
        [SerializeField] private TMP_Text _detailSkill1Description;
        [SerializeField] private TMP_Text _detailSkill2Description;

        private void Awake()
        {
            _curItemBox = ItemBoxs[0];
            _curItemBox.cur = true;

            ItemBoxs[0].data = _player.currentHeadData;
            ItemBoxs[1].data = _player.saveHeadData;




            InputManager.Map map = new InputManager.Map();
            map.AddKeyDownAction(KeyCode.RightArrow, () =>
            {
                if (_boxIndex < ItemBoxs.Length-1)
                {
                    _curItemBox.image.color = new Color(255, 248, 158);
                    _curItemBox.cur = false;
                    _curItemBox = ItemBoxs[++_boxIndex];
                    OnCurChanged?.Invoke();
                    _curItemBox.image.color = new Color(255, 172, 0);
                    _curItemBox.cur = true;
                }
            });
            map.AddKeyDownAction(KeyCode.A, () => { });
            map.AddKeyDownAction(KeyCode.F, () =>
            {
                Debug.Log("UI KeyDown F");
            });
            map.AddKeyPressAction(KeyCode.F, () => { });
            map.AddKeyDownAction(KeyCode.Tab, () =>
            {
                InputManager.instance.currentmap = InputManager.instance.maps["PlayerAction"];
                Hide();
            });
            
            map.AddKeyDownAction(KeyCode.Escape, () =>
            {
                Debug.Log("UI KeyDown ESC");
            });
            map.AddKeyDownAction(KeyCode.D, () =>
            {
                Debug.Log("UI KeyDown D");
            });
            InputManager.instance.AddMap("InventoryUI", map);

            //curItemBox = _player.currentHeadData;


            OnCurChanged += () =>
            {
                switch (_curItemBox.data.type)
                {
                    case Item.ItemType.Head:
                        {
                            HeadItemData data = (HeadItemData)_curItemBox.data;
                            _itemIcon.sprite = data.Icon;
                            _itemName.text = data.Name;
                        }
                        break;
                    case Item.ItemType.Weapon:
                        {

                        }
                        break;
                    case Item.ItemType.Essence:
                        {

                        }
                        break;
                }
            };





        }

        private void OnEnable()
        {
            InputManager.instance.currentmap = InputManager.instance.maps["InventoryUI"];
        }

        



        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }
    }
}