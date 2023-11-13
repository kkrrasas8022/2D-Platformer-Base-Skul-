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
        public bool cur;


        private void Awake()
        {
             image = GetComponent<Image>();
            if(cur)
               image.color = new Color(255, 172, 0);
            else
                image.color = new Color(255, 248, 158);

        }
    }
}