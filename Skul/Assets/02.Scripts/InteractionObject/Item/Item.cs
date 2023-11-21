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
        [SerializeField] private float _maxTop;
        [SerializeField] private bool _goUp;
        [SerializeField] private float _maxBottom;
        [SerializeField]public ItemType type;
        [SerializeField]public ItemData data;
        private SpriteRenderer _renderer;
        [SerializeField]public ItemRate rate;
        [SerializeField]private GameObject _details;
        [SerializeField] public int skillCount;
        [SerializeField] public List<int> skillIDs;

        private void Update()
        {
            if (_renderer.gameObject.transform.position.y > transform.position.y + _maxTop)
                _goUp = false;
            if (_renderer.gameObject.transform.position.y < transform.position.y-_maxBottom)
                _goUp = true;
            if(_goUp==true)
                _renderer.gameObject.transform.position += Vector3.up * Time.deltaTime*0.5f;
            else
                _renderer.gameObject.transform.position += Vector3.down * Time.deltaTime*0.5f;
        }
        private void Awake()
        {
            _renderer = GetComponentInChildren<SpriteRenderer>();
        }
        private void OnEnable()
        {
            if(data != null)
                _renderer.sprite = data.Icon;
            //InitItem(data.rate, data.type, data);
        }

        public void InitItem(ItemRate rate,ItemType type,ItemData data)
        {
            this.type = type;
            this.rate = rate;
            this.data = data;
            _renderer.sprite = data.Icon;
            if(type== ItemType.Head)
            {
                HeadItemData datas = data as HeadItemData;
                skillCount = datas.skillCount;

                for (int i = 0; i < skillCount; i++)
                {
                    int skillid = datas.skulData.activeSkills[Random.Range(0, datas.skulData.activeSkills.Count)].id;
                    if(skillIDs.Count>0)
                    {
                        if (skillIDs[0] == skillid)
                        {
                            i--;
                            continue;
                        }
                    }
                    skillIDs.Add(skillid);
                }
            }
        }
   

        public override void Interaction(Player player)
        {
            base.Interaction(player);
            player.inventory.AddItem(this);
            player.inventory.OnChangeItem?.Invoke(data.id);
            Destroy(gameObject);
        }

        public override void SeeDetails(Player player)
        {
            base.SeeDetails(player);
            if (_details.activeSelf == false)
            { 
                _details.SetActive(true);
            }
        }
        public override void ColseDetails(Player player)
        {
            base.ColseDetails(player);
            if (_details.activeSelf == true)
            { _details.SetActive(false); }
           
        }

    }
}
