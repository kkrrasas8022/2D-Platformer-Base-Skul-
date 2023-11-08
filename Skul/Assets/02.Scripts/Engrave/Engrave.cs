using Skul.Character.PC;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Engrave:MonoBehaviour
{
    protected Player _player;
    public List<int> synergyCount;
    public List<bool> canSynergyEnable;
}
