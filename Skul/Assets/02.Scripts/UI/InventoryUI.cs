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
using System.Diagnostics.Tracing;
using Skul.Tools;
using Skul.Character;

namespace Skul.UI
{ 
    public class InventoryUI : SingletonUIBase<InventoryUI>
    {
        [SerializeField] private Player _player;
        [SerializeField] private GameObject _Skills;
        [SerializeField] private GameObject _inventory;
        [SerializeField] private GameObject _playerStatus;

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
        [SerializeField] private GameObject _detailsObject;
        [SerializeField] private GameObject _detailsSkill1Object;
        [SerializeField] private GameObject _detailsSkill2Object;
        [SerializeField] private Image _detailSkill1Icon;
        [SerializeField] private Image _detailSkill2Icon;
        [SerializeField] private TMP_Text _detailSkill1Name;
        [SerializeField] private TMP_Text _detailSkill2Name;
        [SerializeField] private TMP_Text _detailSkill1CoolTime;
        [SerializeField] private TMP_Text _detailSkill2CoolTime;
        [SerializeField] private TMP_Text _detailSkill1Description;
        [SerializeField] private TMP_Text _detailSkill2Description;

        [Header("Engrave")]
        [SerializeField] private EngraveNotice _engraveNotice;
        [SerializeField] private GameObject _engraveParent;


        protected override void Awake()
        {
            base.Awake();
            _player = GameObject.FindWithTag("Player").GetComponent<Player>();
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
            map.AddKeyDownAction(KeyCode.LeftArrow, () =>
            {
                if (_boxIndex > 0)
                {
                    _curItemBox.image.color = Color.white;
                    OnCurChanged?.Invoke(--_boxIndex);
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
            map.AddKeyPressAction(KeyCode.D, () =>
            {
                Debug.Log("UI KeyDown D");
                _detailsObject.SetActive(true);
                if (_player.currentRen.hadSkillsID.Count> 1)
                {
                    _detailsSkill2Object.SetActive(true);
                }
            });
            map.AddKeyUpAction(KeyCode.D, () =>
            {
                _detailsObject.SetActive(false);
            });
            map.AddKeyPressAction(KeyCode.A, () =>
            {
                Debug.Log("UI KeyDown A");
                PlayerStatusUI.instance.Show();
//                _playerStatus.SetActive(true);
            });
            map.AddKeyUpAction(KeyCode.A, () =>
            {
                PlayerStatusUI.instance.Hide();
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
                _typeTex.text = null;
                _descriptionTex.text = data.description;
                _abilityTex.text = data.abilityDescription;

                switch (data.type)
                {
                    case ItemType.None:
                        {
                            _itemIcon.color = Color.clear;
                            _itemName.text = null;
                            _rateTex.text = null;
                            _typeTex.text= null;
                            _descriptionTex.text = null;
                            _abilityTex.text = null;
                            _Skills.SetActive(false);
                        }
                        break;
                    case ItemType.Head:
                        {
                            PlayerAttacks nowRen;
                            if (value == 0)
                                nowRen = _player.currentRen;
                            else
                                nowRen = _player.subRen;

                            HeadItemData headData = data as HeadItemData;
                            _itemIcon.color = Color.white;
                            _Skills.SetActive(true);
                            _typeTex.text = headData.skulData.skulType.ToString();
                            _switchSkillTex.text = headData.skulData.switchSkill.Name;
                            _skill1Name.text = SkillManager.instance[nowRen.hadSkillsID[0]].Name;
                            _skill1Icon.sprite = SkillManager.instance[nowRen.hadSkillsID[0]].Icon;
                            _detailSkill1Icon.sprite = SkillManager.instance[nowRen.hadSkillsID[0]].Icon;
                            _detailSkill1Name.text = SkillManager.instance[nowRen.hadSkillsID[0]].Name;
                            _detailSkill1CoolTime.text = ((ActiveSkillData)SkillManager.instance[nowRen.hadSkillsID[0]]).CoolTime.ToString() + "s";
                            _detailSkill1Description.text = SkillManager.instance[nowRen.hadSkillsID[0]].Description;

                            if (headData.skillCount == 1)
                            {
                                _skill2Icon.gameObject.SetActive(false);
                                _detailsObject.SetActive(true);
                                _detailsSkill2Object.SetActive(false);
                                _detailsObject.SetActive(false);
                                _skill1Icon.transform.localPosition = new Vector3(0, _skill1Icon.transform.localPosition.y, 0);
                                _detailsSkill1Object.transform.localPosition=new Vector3(0, _detailsSkill1Object.transform.localPosition.y, 0);
                            }
                            else
                            {
                                _skill2Icon.gameObject.SetActive(true);
                                _detailsObject.SetActive(true);
                                _detailsSkill2Object.SetActive(true);
                                _detailsObject.SetActive(false);
                                _skill2Name.text = SkillManager.instance[nowRen.hadSkillsID[1]].Name;
                                _skill2Icon.sprite = SkillManager.instance[nowRen.hadSkillsID[1]].Icon;
                                _skill1Icon.transform.localPosition = new Vector3(-188, _skill1Icon.transform.localPosition.y, 0);
                                _detailsSkill1Object.transform.localPosition=new Vector3(-161, _detailsSkill1Object.transform.localPosition.y, 0);

                                _detailSkill2Icon.sprite = SkillManager.instance[nowRen.hadSkillsID[1]].Icon;
                                _detailSkill2Name.text = SkillManager.instance[nowRen.hadSkillsID[1]].Name;
                                _detailSkill2CoolTime.text = ((ActiveSkillData)SkillManager.instance[nowRen.hadSkillsID[1]]).CoolTime.ToString() + "s";
                                _detailSkill2Description.text = SkillManager.instance[nowRen.hadSkillsID[1]].Description;
                            }
                        }
                        break;
                    case ItemType.Weapon:
                        {
                            WeaponItemData weaponData = data as WeaponItemData;
                            _itemIcon.color = Color.white;
                            _Skills.SetActive(true);
                            _typeTex.text = null;
                            _switchSkillTex.text = "Engrave";
                            _skill1Name.text = weaponData.engraves[0].Name;
                            _skill2Name.text = weaponData.engraves[1].Name;
                            _skill1Icon.sprite = weaponData.engraves[0].Icon;
                            _skill2Icon.sprite = weaponData.engraves[1].Icon;
                            _detailSkill1Icon.sprite = weaponData.engraves[0].Icon;
                            _detailSkill1Name.text = weaponData.engraves[0].Name;
                            _detailSkill1CoolTime.text = "";
                            _detailSkill1Description.text = weaponData.engraves[0].synergyAbility;
                            _skill2Icon.gameObject.SetActive(true);
                            _detailsObject.SetActive(true);
                            _detailsSkill2Object.SetActive(true);
                            _detailsObject.SetActive(false);
                            _skill2Name.text = weaponData.engraves[1].Name;
                            _skill2Icon.sprite = weaponData.engraves[1].Icon;
                            _skill1Icon.transform.localPosition = new Vector3(-188, _skill1Icon.transform.localPosition.y, 0);
                            _detailsSkill1Object.transform.localPosition = new Vector3(-161, _detailsSkill1Object.transform.localPosition.y, 0);

                            _detailSkill2Icon.sprite = weaponData.engraves[1].Icon;
                            _detailSkill2Name.text = weaponData.engraves[1].Name;
                            _detailSkill2CoolTime.text = "";
                            _detailSkill2Description.text = weaponData.engraves[1].synergyAbility;

                        }
                        break;
                    case ItemType.Essence:
                        {
                            EssenceItemData essenceData = data as EssenceItemData;
                            _itemIcon.color = Color.white;
                            _Skills.SetActive(false);
                            _typeTex.text = null;
                            _switchSkillTex.text = null;
                            _skill1Name.text = null;
                            _skill2Name.text = null;
                            _skill1Icon.sprite = null;
                            _skill2Icon.sprite = null;
                        }
                        break;
                }
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
            OnCurChanged?.Invoke(_boxIndex);
            _curItemBox.image.color = new Color(255, 172, 0);

            if(_player.inventory.HaveEngrave.Count>0)
            {
                int i = 0;
                foreach(var item in _player.inventory.HaveEngrave)
                {
                    if (i >= 9)
                        break;
                    EngraveNotice notice = Instantiate(_engraveNotice, _engraveParent.transform);
                    notice.gameObject.SetActive(false);
                    notice.transform.localPosition=new Vector3(0,270-60*i,0);
                    notice.data = item.Key;
                    notice.engraveCount = item.Value;
                    i++;
                    notice.gameObject.SetActive(true);
                }
            }



            InputManager.instance.currentmap = InputManager.instance.maps["InventoryUI"];
            Debug.Log("OnEnable End");
        }

        



        public override void Hide()
        {
            base.Hide();
            _curItemBox.image.color = Color.white;
            PauseController.instance.OnPause?.Invoke();
        }

        public override void Show()
        {
            base.Show();
            
        }
    }
}