using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Skul.Tools;

namespace Skul.Data
{
    public class DataManager : SingletonMonoBase<DataManager>
    {
        public Dictionary<int, ItemData> itemDatum;
        public ItemData this[int id] => itemDatum[id];

        [SerializeField] private List<HeadItemData> _headItemDatum;
        [SerializeField] private List<EssenceItemData> _essenceItemDatum;
        [SerializeField] private List<WeaponItemData> _weaponItemDatum;

        private void Awake()
        {
            itemDatum = new Dictionary<int, ItemData>();

            foreach (var item in _headItemDatum) 
            {
                itemDatum.Add(item.id, item);
            }

            foreach (var item in _essenceItemDatum)
            {
                itemDatum.Add(item.id, item);
            }

            foreach (var item in _weaponItemDatum)
            {
                itemDatum.Add(item.id, item);
            }
        }
    }
}