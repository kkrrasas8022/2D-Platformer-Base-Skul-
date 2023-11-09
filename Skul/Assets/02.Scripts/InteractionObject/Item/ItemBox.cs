using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Skul.Data;
using Skul.Character.PC;

namespace Skul.Item
{
    public class ItemBox : InteractionObject
    {
        [SerializeField] private ItemRate _rate;
        [SerializeField] private ItemType _type;
        [SerializeField] private Item _item;
        [SerializeField] private ItemTable _table;

        public override void Interaction(Player player)
        {
            base.Interaction(player);
            Debug.Log("box");
            Instantiate(_item,transform.position, Quaternion.identity).InitItem(_rate, _type, _table.itemDatas[Random.Range(0,_table.itemDatas.Count)]);
            Destroy(this.gameObject);
        }
        public override void SeeDetails(Player player)
        {
            base.SeeDetails(player);
        }
    }
}