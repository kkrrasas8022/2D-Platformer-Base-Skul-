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


        public void AddItem(int itemID)
        {
            ItemData data = DataManager.instance[itemID];
            switch (data.type)
            {
                case Item.ItemType.Head:
                    {
                        if (_saveHeadData == null)
                        {
                            _player._currentRen.gameObject.SetActive(false);
                            _saveHeadData = _curHeadData;
                            _curHeadData.DeleteAbility(_player);
                            _curHeadData=data as HeadItemData;
                            _curHeadData.HadAbility(_player);
                            _player._currentRen=Instantiate(_curHeadData.skulData.Renderer, transform);
                            _player._renderers.Add(_player._currentRen);
                            _player.AnimatorChange(_player._currentRen.GetComponent<Animator>());
                            OnHeadAdd?.Invoke(data as HeadItemData);
                        }
                        else
                        {
                            _curHeadData.DeleteAbility(_player);
                            DropItem(_curHeadData);
                            Destroy(_player._currentRen.gameObject);
                            _player._renderers.Remove(_curHeadData.skulData.Renderer);
                            _curHeadData = data as HeadItemData;
                            _curHeadData.HadAbility(_player);
                            PlayerAttacks curren = Instantiate(_curHeadData.skulData.Renderer, transform);
                            curren.InitAttackRenderer();
                            PlayerAttacks temp = (_player._currentRen==_player._renderers[0]?_player._renderers[1]:_player._renderers[0]);
                            _player._renderers.Clear();
                            _player._renderers.Add(curren);
                            _player._renderers.Add(temp);
                            _player._currentRen = curren;
                            _player.AnimatorChange(curren.GetComponent<Animator>());    
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