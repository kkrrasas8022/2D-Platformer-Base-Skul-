using Skul.Character.PC;
using Skul.Data;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Skul.UI
{
    public class InventoryBox:MonoBehaviour
    {
        public ItemData data;
        public Image image;
        private Image icon;

        private void Awake()
        {
           
           image = GetComponent<Image>();
            //icon.sprite = data.Icon;
        }

        
    }
}