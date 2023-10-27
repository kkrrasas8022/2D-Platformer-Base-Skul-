using Skul.Character.PC;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

[CreateAssetMenu(fileName ="Skul Data",menuName ="Scriptable Object Asset/Skuls")]
public class SkulData : ScriptableObject
{
    public enum SkulType
    {
        Balance,
        Power,
        Speed,
    }
    public AnimatorController animator;
    public SkulType type;
}
