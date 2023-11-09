using Skul.Data;
using Skul.Item;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DropItemUI : MonoBehaviour
{
    private Item _items;

    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _itemForce;
    [SerializeField] private TMP_Text _itemRate;
    [SerializeField] private TMP_Text _essenceCoolTime;
    [SerializeField] private GameObject _detailsNotice;
    [SerializeField] private GameObject _details;

    [SerializeField] private TMP_Text _skill1Name;
    [SerializeField] private TMP_Text _skill2Name;
    [SerializeField] private TMP_Text _detailSkill1Name;
    [SerializeField] private TMP_Text _detailSkill2Name;
    [SerializeField] private TMP_Text _detailSkill1CoolTime;
    [SerializeField] private TMP_Text _detailSkill2CoolTime;
    [SerializeField] private TMP_Text _detailSkill1Des;
    [SerializeField] private TMP_Text _detailSkill2Des;

    [SerializeField] private Image _icon1; 
    [SerializeField] private Image _icon2; 
    [SerializeField] private Image _detailIcon1; 
    [SerializeField] private Image _detailIcon2; 


    private void Awake()
    {
        _items= GetComponentInParent<Item>();
        switch (_items.type)
        {
            case ItemType.Head:
                {
                    HeadItemData data = (HeadItemData)_items.data;
                    _name.text = data.skulData.Name;
                }
                break;
            case ItemType.Weapon:
                {
                    WeaponItemData data = (WeaponItemData)_items.data;
                    _name.text=data.Name;
                    _itemForce.text = data.abilityDescription;
                    _itemRate.text =  data.rate.ToString();
                    _essenceCoolTime.gameObject.SetActive(false);
                    _detailsNotice.gameObject.SetActive(true);
                    _details.gameObject.SetActive(false);

                    _skill1Name.text = data.engraves[0].Name;
                    _skill2Name.text = data.engraves[1].Name;

                    _icon1.sprite = data.engraves[0].Icon;
                    _icon2.sprite = data.engraves[1].Icon;
                }
                break;
            case ItemType.Essence:
                break;
        }


    }
}