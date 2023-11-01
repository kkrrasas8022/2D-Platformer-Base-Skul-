using Skul.Data;
using System.Collections;
using System.Collections.Generic;
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
        public ItemType Type;
        public ItemData Data;

        private SpriteRenderer _renderer;

        private void Awake()
        {
            _renderer = GetComponentInChildren<SpriteRenderer>();
            _renderer.sprite = Data.Icon;
        }
    } 
}
