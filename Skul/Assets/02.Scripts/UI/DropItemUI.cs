using Skul.Data;
using Skul.Item;
using System.Collections;
using System.Collections.Generic;
using System.Data;
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
            case ItemType.None:
                break;
            case ItemType.Head:
                {
                    HeadItemData data = (HeadItemData)_items.data;
                    _name.text = data.skulData.Name;
                    _itemForce.text = data.abilityDescription;
                    _itemRate.text = data.rate.ToString();
                    _essenceCoolTime.text = data.skulData.skulType.ToString();
                    _detailsNotice.gameObject.SetActive(true);
                    _details.gameObject.SetActive(false);

                    _skill1Name.text = SkillManager.instance[_items.skillIDs[0]].Name;
                    _icon1.sprite = SkillManager.instance[_items.skillIDs[0]].Icon;
                    _detailIcon1.sprite= SkillManager.instance[_items.skillIDs[0]].Icon;
                    _detailSkill1CoolTime.text = ((ActiveSkillData)SkillManager.instance[_items.skillIDs[0]]).CoolTime.ToString();
                    _detailSkill1Des.text = SkillManager.instance[_items.skillIDs[0]].Description;
                    _detailSkill1Name.text= SkillManager.instance[_items.skillIDs[0]].Name;

                    if(_items.skillCount>1)
                    {
                        _skill2Name.text = SkillManager.instance[_items.skillIDs[1]].Name;
                        _icon2.sprite = SkillManager.instance[_items.skillIDs[1]].Icon;
                        _detailIcon2.sprite = SkillManager.instance[_items.skillIDs[1]].Icon;
                        _detailSkill2CoolTime.text = ((ActiveSkillData)SkillManager.instance[_items.skillIDs[1]]).CoolTime.ToString();
                        _detailSkill2Des.text = SkillManager.instance[_items.skillIDs[1]].Description;
                        _detailSkill2Name.text = SkillManager.instance[_items.skillIDs[1]].Name;
                    }
                    
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
                    _detailSkill1Name.text = data.engraves[0].Name;
                    _detailSkill2Name.text = data.engraves[1].Name;
                    _detailSkill1CoolTime.gameObject.SetActive(false);
                    _detailSkill2CoolTime.gameObject.SetActive(false);
                    _detailSkill1Des.text = data.engraves[0].synergyAbility +
                        $"\n{(data.engraves[0].synergyPower[0].power)*100}%/{(data.engraves[0].synergyPower[1].power)*100}%";
                    _detailSkill2Des.text = data.engraves[1].synergyAbility +
                        $"\n{(data.engraves[1].synergyPower[0].power) * 100}%/{(data.engraves[1].synergyPower[1].power) * 100}%"; ;


                    _icon1.sprite = data.engraves[0].Icon;
                    _icon2.sprite = data.engraves[1].Icon;
                    _detailIcon1.sprite = data.engraves[0].Icon;
                    _detailIcon2.sprite = data.engraves[1].Icon;

                }
                break;
            case ItemType.Essence:
                break;
        }


    }
}