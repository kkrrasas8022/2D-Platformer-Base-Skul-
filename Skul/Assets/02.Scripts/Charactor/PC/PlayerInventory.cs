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
        public HeadItemData CurHeadData => _curHeadData;
        public HeadItemData SaveHeadData => _saveHeadData;
        public EssenceItemData EssenceData => _essenceData;
        public List<WeaponItemData> WeaponDatas => _weaponDatas;

        //������ �ִ� ������ �̸��� Key �� ������ ��ø���� Value�� ������ Dictionary 
        private Dictionary<Engrave, int> _haveEngrave;
        public Dictionary<Engrave, int> HaveEngrave=> _haveEngrave;
        public int enghcount;
        //���ο� ������ ������ �� ��Ÿ���� ȿ��
        public event Action<Engrave, int> OnEngraveChange;
        public Action<int> OnChangeItem;

        [SerializeField]private HeadItemData _curHeadData;
        [SerializeField]private HeadItemData _saveHeadData;
        [SerializeField]private EssenceItemData _essenceData;
        [SerializeField]private List<WeaponItemData> _weaponDatas;


        public void AddEngrave(Engrave engrave)
        {
            if (!_haveEngrave.TryAdd(engrave, 1))
                _haveEngrave[engrave]++;
        }



        private void Awake()
        {
            _weaponDatas = new List<WeaponItemData>();  
            _haveEngrave = new Dictionary<Engrave, int>();
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
                        if (_weaponDatas.Count < 9)
                        { 
                            _weaponDatas.Add(data as WeaponItemData);
                            AddEngrave((data as WeaponItemData).engraves[0]);
                            AddEngrave((data as WeaponItemData).engraves[1]);
                        }
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