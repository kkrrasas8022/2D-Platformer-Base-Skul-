using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Skul.Data
{
    [CreateAssetMenu(fileName = "WeaponItemData", menuName = "Skul/Item/Weapon")]
    public class WeaponItemData : ItemData
    {
        public Engrave[] engraves = new Engrave[2];
    }
}