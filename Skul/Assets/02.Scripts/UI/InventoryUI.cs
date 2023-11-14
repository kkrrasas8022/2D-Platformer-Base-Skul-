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
using Skul.Item;

namespace Skul.UI
{ 
    public class InventoryUI : MonoBehaviour, IUI
    {
        [SerializeField] private Player _player;
        [SerializeField] private GameObject _Skills;
        [SerializeField] private GameObject _inventory;

        [Header("Inventory")]
        [SerializeField] private InventoryBox _curItemBox;
        public event Action<int> OnCurChanged;
        [SerializeField] private int _boxIndex;
        [SerializeField] private InventoryBox _curHeadItemBox;
        [SerializeField] private InventoryBox _subHeadItemBox;
        [SerializeField] private InventoryBox _essenceItemBox;
        [SerializeField] private InventoryBox[] weaponItemBoxes;
        [SerializeField] private InventoryBox _itemBoxPrefab;

        [Header("Description")]
        [SerializeField] private Image      _itemIcon;
        [SerializeField] private TMP_Text   _itemName;
        [SerializeField] private TMP_Text   _rateTex;
        [SerializeField] private TMP_Text   _typeTex;
        [SerializeField] private TMP_Text   _descriptionTex;
        [SerializeField] private TMP_Text   _abilityTex;
        [SerializeField] private TMP_Text   _switchSkillTex;
        [SerializeField] private TMP_Text   _skill1Name;
        [SerializeField] private TMP_Text   _skill2Name;
        [SerializeField] private Image      _skill1Icon;
        [SerializeField] private Image      _skill2Icon;


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
            weaponItemBoxes = new InventoryBox[9];

            _curHeadItemBox = Instantiate(_itemBoxPrefab, _inventory.transform);
            _curHeadItemBox.transform.localPosition = new Vector3(-40.0f, 177.0f, 0.0f);
            _curHeadItemBox.gameObject.SetActive(false);

            _subHeadItemBox = Instantiate(_itemBoxPrefab, _inventory.transform);
            _subHeadItemBox.transform.localPosition = new Vector3(40.0f, 177.0f, 0.0f);
            _subHeadItemBox.gameObject.SetActive(false);


            _essenceItemBox = Instantiate(_itemBoxPrefab, _inventory.transform);
            _essenceItemBox.transform.localPosition = new Vector3(0.0f, 45.0f, 0.0f);
            _essenceItemBox.gameObject.SetActive(false);


            for (int i = 0; i < weaponItemBoxes.Length; i++)
            {
                weaponItemBoxes[i] = Instantiate(_itemBoxPrefab, _inventory.transform);
                weaponItemBoxes[i].transform.localPosition = new Vector3(-60.0f + (60.0f * (i % 3)), -80.0f + (-53.0f * (i / 3)), 0.0f);
                weaponItemBoxes[i].gameObject.SetActive(false);
            }

            InputManager.Map map = new InputManager.Map();
            map.AddKeyDownAction(KeyCode.RightArrow, () =>
            {
                if (_boxIndex < 12)
                {
                    _curItemBox.image.color = Color.white;
                    OnCurChanged?.Invoke(++_boxIndex);
                    _curItemBox.image.color = new Color(255, 172, 0);
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


            
                

            OnCurChanged += (value) =>
            {
                switch(value)
                {
                    case 0:
                        _curItemBox = _curHeadItemBox;
                        break;
                    case 1:
                        _curItemBox= _subHeadItemBox;
                        break;
                    case 2:
                        _curItemBox = _essenceItemBox; 
                        break;
                    case 3:
                    case 4:
                    case 5:
                    case 6:
                    case 7:
                    case 8:
                    case 9:
                    case 10:
                    case 11:
                        _curItemBox = weaponItemBoxes[value - 3];
                        break;
                }
                ItemData data = DataManager.instance[_curItemBox.dataID];
                _itemIcon.sprite = data.Icon;
                _itemName.text=data.Name;
                _rateTex.text = data.rate.ToString();
                _descriptionTex.text = data.description;
                _abilityTex.text = data.abilityDescription;

                //if (data is HeadItemData)
                //{
                //    SkulData skulData = (data as HeadItemData).skulData;
                //    _typeTex.text=data.type.ToString();
                //    _switchSkillTex.text = skulData.switchSkill.Name;
                //    _skill1Name.text = skulData.activeSkills[0].Name;
                //    _skill2Name.text=skulData.activeSkills[1].Name;
                //    _skill1Icon.sprite=skulData.activeSkills[0].Icon;
                //    _skill2Icon.sprite=skulData.activeSkills[1].Icon;
                //}


            };


            Debug.Log("Awake End");


        }

        private void OnEnable()
        {
            Debug.Log("OnEnable Start");
            // _curItemBox.image.color = Color.white;
      

            if (_player.inventory.CurHeadData != null)
                _curHeadItemBox.dataID = _player.inventory.CurHeadData.id;
            else
                _curHeadItemBox.dataID = 0;
            _curHeadItemBox.gameObject.SetActive(true);

            if (_player.inventory.SaveHeadData != null)
                _subHeadItemBox.dataID = _player.inventory.SaveHeadData.id;
            else
                _subHeadItemBox.dataID = 0;
            _subHeadItemBox.gameObject.SetActive(true);

            if (_player.inventory.EssenceData != null)
                _essenceItemBox.dataID = _player.inventory.EssenceData.id;
            else
                _essenceItemBox.dataID = 0;
            _essenceItemBox.gameObject.SetActive(true);

            for (int i = 0; i < weaponItemBoxes.Length; i++)
            {
                if (_player.inventory.WeaponDatas.Count-1<i)
                    weaponItemBoxes[i].dataID = 0;
                else
                    weaponItemBoxes[i].dataID = _player.inventory.WeaponDatas[i].id;
                weaponItemBoxes[i].gameObject.SetActive(true);
            }

            _boxIndex = 0;
            OnCurChanged(_boxIndex);
            _curItemBox.image.color = new Color(255, 172, 0);

            InputManager.instance.currentmap = InputManager.instance.maps["InventoryUI"];
            Debug.Log("OnEnable End");
        }

        



        public void Hide()
        {
            gameObject.SetActive(false);
            _curItemBox.image.color = Color.white;
        }

        public void Show()
        {
            gameObject.SetActive(true);
            //OnEnable();
        }
    }
}