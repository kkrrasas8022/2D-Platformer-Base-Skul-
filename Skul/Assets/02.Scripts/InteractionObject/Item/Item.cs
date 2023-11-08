using Skul.Character.PC;
using Skul.Data;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Skul.Item
{
    public enum ItemType
    {
        Head,
        Weapon,
        Essence,
    }
    public class Item : InteractionObject
    {
        
        [SerializeField]private ItemType _type;
        [SerializeField]private ItemData _data;
        private SpriteRenderer _renderer;
        [SerializeField]private ItemRate _rate;

        private void Awake()
        {
            _renderer = GetComponentInChildren<SpriteRenderer>();
        }
        public void InitItem(ItemRate rate,ItemType type,ItemData data)
        {
            _type = type;
            _rate = rate;
            _data = data;
            _renderer.sprite = _data.Icon;
        }
        public void GainItem(Player player)
        {
            switch (_type)
            {
                case ItemType.Head:
                    break;
                case ItemType.Weapon:
                    {
                        player.haveEngrave.Add(((WeaponItemData)_data).engraves[0], 1);
                    }
                    break;
                case ItemType.Essence:
                    break;
            }
        }

        public override void Interaction(Player player)
        {
            base.Interaction(player);
            player.items.Add((WeaponItemData)_data);
            Destroy(gameObject);
        }

    }
}
