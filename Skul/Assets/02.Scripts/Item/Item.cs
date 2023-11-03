using Skul.Data;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Skul.Item
{
    public class Item : MonoBehaviour
    {
        public enum ItemType
        {
            Head,
            Weapon,
            Essence,
        }
        [SerializeField]private ItemType _type;
        public ItemData data;
        private SpriteRenderer _renderer;
        [SerializeField]private ItemRate _rate;

        private void OnEnable()
        {
            _renderer = GetComponentInChildren<SpriteRenderer>();
            _renderer.sprite = data.Icon;
        }
        public void InitItem(ItemRate rate,ItemType type)
        {
            _type = type;
            _rate = rate;
        }
    } 
}
