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
        None,
        Head,
        Weapon,
        Essence,
    }
    public class Item : InteractionObject
    {
        
        [SerializeField]public ItemType type;
        [SerializeField]public ItemData data;
        private SpriteRenderer _renderer;
        [SerializeField]public ItemRate rate;
        [SerializeField]private GameObject _details;

        private void Awake()
        {
            _renderer = GetComponentInChildren<SpriteRenderer>();
            
        }
        private void OnEnable()
        {
            if(data != null)
                _renderer.sprite = data.Icon;
        }

        public void InitItem(ItemRate rate,ItemType type,ItemData data)
        {
            this.type = type;
            this.rate = rate;
            this.data = data;
            _renderer.sprite = data.Icon;
        }
   

        public override void Interaction(Player player)
        {
            base.Interaction(player);
            player.inventory.AddItem(data.id);
            player.inventory.OnChangeItem?.Invoke(data.id);
            Destroy(gameObject);
        }

        public override void SeeDetails(Player player)
        {
            base.SeeDetails(player);
            if(_details.activeSelf==true)
            { _details.SetActive(false);}
            else if (_details.activeSelf == false)
            { _details.SetActive(true); }
        }


    }
}
