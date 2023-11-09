using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Skul.Data
{
    [CreateAssetMenu(fileName = "WeaponItemData", menuName = "Skul/Item/Weapon")]
    public class WeaponItemData : ItemData
    {
        public Engrave[] engraves = new Engrave[2];
        [SerializeField] public Power power;
    }
    [Serializable]
    public class Power
    {
        public StatusType type;
        public float power;
    }
}