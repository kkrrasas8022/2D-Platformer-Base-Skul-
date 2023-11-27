using Skul.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuffData", menuName = "Skul/Buff")]
public class BuffData : ScriptableObject
{
    public Sprite icon;
    public Power power;
    public float MaxTime;
}
