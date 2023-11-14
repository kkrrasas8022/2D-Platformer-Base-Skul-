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

        private void Awake()
        {
            image = GetComponent<Image>();
            //inventory.OnChangeItem(dataID);
        }
        private void OnEnable()
        {
            Debug.Log("Box");
        }


    }
}