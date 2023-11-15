using Skul.Data;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Skul.UI
{
    public class EngraveNotice : MonoBehaviour
    {
        public Engrave data;

        public Image icon;
        public int engraveCount;
        public List<int> senergyCount;
        
        public TMP_Text engraveCountTex;
        public TMP_Text engraveName;
        public TMP_Text engraveSenergyCount;


        private void Awake()
        {
            
        }
        private void OnEnable()
        {
            if (data != null)
            {
                icon.sprite = data.Icon;
                engraveName.text = data.Name;
                engraveCountTex.text=engraveCount.ToString();
                senergyCount = data.synergyCount;
                string Text="";
                for(int i=0; i<senergyCount.Count;i++)
                {
                    if(i==senergyCount.Count-1)
                    {
                        Text = Text + $"{senergyCount[i]}";
                        break;
                    }
                    Text = Text + $"{senergyCount[i]} > ";
                }
                engraveSenergyCount.text = Text;
            }
        }
    }
}