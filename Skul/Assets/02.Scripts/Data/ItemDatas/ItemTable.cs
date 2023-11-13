using Skul.Item;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Skul.Data 
{
    [CreateAssetMenu(fileName = "ItemDataTable", menuName = "ItemDataTable")]
    public class ItemTable : ScriptableObject
    {
       public ItemType type;
       public List<ItemData> itemDatas = new List<ItemData>();
    } 
}
