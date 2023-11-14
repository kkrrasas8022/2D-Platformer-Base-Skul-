using Skul.Data;
using Skul.UI;
using System;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Skul.Character.PC
{
    public class PlayerInventory:MonoBehaviour
    {
        public HeadItemData curHeadData => _curHeadData;
        public HeadItemData saveHeadData => _saveHeadData;
        public EssenceItemData EssenceData => _essenceData;
        public List<WeaponItemData> weaponDatas => _weaponDatas;

        //������ �ִ� ������ �̸��� Key �� ������ ��ø���� Value�� ������ Dictionary 
        public Dictionary<Engrave, int> haveEngrave;
        public int enghcount;
        //���ο� ������ ������ �� ��Ÿ���� ȿ��
        public event Action<Engrave, int> OnEngraveChange;
        public Action<int> OnChangeItem;

        [SerializeField]private HeadItemData _curHeadData;
        [SerializeField]private HeadItemData _saveHeadData;
        [SerializeField]private EssenceItemData _essenceData;
        [SerializeField]private List<WeaponItemData> _weaponDatas;

        private void Awake()
        {
            _weaponDatas = new List<WeaponItemData>();  
        }

        public void SwitchHead()
        {
            HeadItemData tmpData;
            tmpData = _curHeadData;
            _curHeadData = _saveHeadData;
            _saveHeadData = tmpData;
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
                            _saveHeadData = _curHeadData;
                            _curHeadData=data as HeadItemData;
                        }
                        else
                        {
                            throw new Exception("head�� 2���� ����");
                            //itemChange
                        }
                    }
                    break;
                case Item.ItemType.Weapon:
                    {
                        if (_weaponDatas.Count<9)
                             _weaponDatas.Add(data as WeaponItemData);
                        else
                        {
                            throw new Exception("weapon�� 9���� ����");
                            //ItemChange
                        }
                    }
                    break;
                case Item.ItemType.Essence:
                    { 
                        _essenceData = data as EssenceItemData;
                        //DropItem;
                    }
                    break;
            }
        }
    }
}