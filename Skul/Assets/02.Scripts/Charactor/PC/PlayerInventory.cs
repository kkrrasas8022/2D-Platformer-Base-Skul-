using Skul.Data;
using Skul.Item;
using Skul.UI;
using System;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Skul.Character.PC
{
    public class PlayerInventory:MonoBehaviour
    {
        [SerializeField] private Player _player;
        [SerializeField] private Item.Item _dropItem;
        public HeadItemData CurHeadData => _curHeadData;
        public HeadItemData SaveHeadData => _saveHeadData;
        public EssenceItemData EssenceData => _essenceData;
        public List<WeaponItemData> WeaponDatas => _weaponDatas;

        //가지고 있는 각인의 이름을 Key 그 각인의 중첩수를 Value로 가지는 Dictionary 
        private Dictionary<Engrave, int> _haveEngrave;
        public Dictionary<Engrave, int> HaveEngrave=> _haveEngrave;
        public int enghcount;
        //각인에 변동이 생겼을 때 나타나는 효과
        public event Action<Engrave, int> OnEngraveChange;
        public Action<int> OnChangeItem;

        [SerializeField]private HeadItemData _curHeadData;
        [SerializeField]private HeadItemData _saveHeadData;
        [SerializeField]private EssenceItemData _essenceData;
        [SerializeField]private List<WeaponItemData> _weaponDatas;
        public event Action<HeadItemData> OnHeadAdd;

        public void DropItem(ItemData data)
        {
            Item.Item tem = Instantiate(_dropItem, new Vector3(transform.position.x,transform.position.y+0.5f), Quaternion.identity);
            tem.InitItem(data.rate, data.type, data);
            if (data.type == ItemType.Head)
            { tem.skillIDs = _player.currentRen.hadSkillsID; }
        }


        public void AddEngrave(Engrave engrave)
        {
            if (!_haveEngrave.TryAdd(engrave, 1))
                _haveEngrave[engrave]++;
        }
        public void RemoveEngrave(Engrave engrave) 
        {
            if (_haveEngrave[engrave]>1)
                _haveEngrave[engrave]--;
            else
                _haveEngrave.Remove(engrave);
        }



        private void Awake()
        {
            _weaponDatas = new List<WeaponItemData>();  
            _haveEngrave = new Dictionary<Engrave, int>();
        }

        private void Start()
        {
            _curHeadData.HadAbility(_player);
        }

        public void SwitchHead()
        {
            _curHeadData.DeleteAbility(_player);
            HeadItemData tmpData;
            tmpData = _curHeadData;
            _curHeadData = _saveHeadData;
            _saveHeadData = tmpData;
            _curHeadData.HadAbility(_player);
        }


        public void AddItem(Item.Item item)
        {
            ItemData data = DataManager.instance[item.data.id];
            switch (data.type)
            {
                case Item.ItemType.Head:
                    {
                        HeadItemData headdata = data as HeadItemData;
                        if (_saveHeadData == null)
                        {
                            _player.subRen = _player.currentRen;
                            _player.currentRen.gameObject.SetActive(false);
                            _saveHeadData = _curHeadData;
                            _curHeadData.DeleteAbility(_player);
                            _curHeadData=headdata;
                            _player.currentRen=Instantiate(_curHeadData.skulData.Renderer, transform);
                            _player.currentRen.hadSkillsID = item.skillIDs;
                            _player.renderers.Add(_player.currentRen);
                            Animator ani = _player.currentRen.GetComponent<Animator>();
                            _player.AnimatorChange(ani);
                            _curHeadData.HadAbility(_player);
                            OnHeadAdd?.Invoke(headdata);
                        }
                        else
                        {
                            _curHeadData.DeleteAbility(_player);
                            DropItem(_curHeadData);
                            Destroy(_player.currentRen.gameObject);
                            _player.renderers.Remove(_curHeadData.skulData.Renderer);
                            _curHeadData = data as HeadItemData;

                            PlayerAttacks curren = Instantiate(_curHeadData.skulData.Renderer, transform);
                            curren.InitAttackRenderer();
                            curren.hadSkillsID = item.skillIDs;
                            PlayerAttacks temp = (_player.currentRen==_player.renderers[0]?_player.renderers[1]:_player.renderers[0]);
                            _player.renderers.Clear();
                            _player.renderers.Add(curren);
                            _player.renderers.Add(temp);
                            _player.currentRen = curren;
                            _player.AnimatorChange(curren.GetComponent<Animator>());
                            _curHeadData.HadAbility(_player);
                            OnHeadAdd?.Invoke(headdata);
                        }
                        
                    }
                    break;
                case Item.ItemType.Weapon:
                    {
                        WeaponItemData weapondata = data as WeaponItemData;
                        if (_weaponDatas.Count < 9)
                        { 
                            _weaponDatas.Add(weapondata);
                            data.HadAbility(_player);
                            AddEngrave(weapondata.engraves[0]);
                            weapondata.engraves[0].ActivationSynergy(_player, _haveEngrave[weapondata.engraves[0]]);
                            AddEngrave(weapondata.engraves[1]);
                            weapondata.engraves[1].ActivationSynergy(_player, _haveEngrave[weapondata.engraves[1]]);
                        }
                        else
                        {
                            DropItem(_weaponDatas[0]);
                            _weaponDatas[0].DeleteAbility(_player);
                            _weaponDatas[0]= data as WeaponItemData;
                            data.HadAbility(_player);
                        }
                    }
                    break;
                case Item.ItemType.Essence:
                    {
                        DropItem(_essenceData);
                        _essenceData = data as EssenceItemData;
                    }
                    break;
            }
        }
    }
}