using JetBrains.Annotations;
using Skul.Character.PC;
using Skul.GameManager;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PotalType
{
    Money,
    Bone,
    Weapon,
    MiddleBoss,
    Boss,
    Shop,
    Broken
}
public class Potal : InteractionObject
{
    [SerializeField] private PotalType type; 
    public override void Interaction(Player player)
    {
        if (type == PotalType.Broken)
            return;
        if (GameManager.instance.isClear == false)
            return;
        base.Interaction(player);
        GameManager.instance.GoNextMap();
        Debug.Log("Go Next Map");
    }

    public override void SeeDetails(Player player)
    {
        if (type == PotalType.Broken)
            return;
        base.SeeDetails(player);
    }
}
