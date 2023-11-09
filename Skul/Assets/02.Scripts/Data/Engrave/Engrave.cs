using Skul.Character.PC;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Skul.Data
{
    [CreateAssetMenu(fileName = ("Engraves"), menuName = ("Skul/Engrave"))]
    public class Engrave : ScriptableObject
    {
        public string Name;
        public string synergyAbility;
        public List<int> synergyCount;
        public List<Power> synergyPower;
        public Sprite Icon;
    }
}
