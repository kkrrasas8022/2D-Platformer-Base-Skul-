using Skul.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour,IUI
{
    [SerializeField] private GameObject _headDescription;
    [SerializeField] private GameObject _essenceDescription;
    [SerializeField] private GameObject _weaponDescription;





    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    
}
