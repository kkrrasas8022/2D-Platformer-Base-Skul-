using Skul.Character.PC;
using Skul.Data;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Skul.UI
{
    public class InventoryBox:MonoBehaviour
    {

        public int dataID;
        public Image image;
        public Image icon;

        private void Awake()
        {
            image = GetComponent<Image>();
        }
        private void OnEnable()
        {
            icon.sprite = DataManager.instance[dataID].Icon;
        }


    }
}