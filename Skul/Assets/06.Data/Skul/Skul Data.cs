using Skul.Character.PC;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public enum SkulType
{
    Balance,
    Power,
    Speed,
}
[CreateAssetMenu(fileName ="Skul Data",menuName ="Scriptable Object Asset/Skuls")]
public class SkulData : ScriptableObject
{
    public GameObject Renderer;
    public SkulType type;
}
