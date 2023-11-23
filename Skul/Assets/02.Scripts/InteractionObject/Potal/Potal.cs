using JetBrains.Annotations;
using Skul.Character.PC;
using Skul.GameManager;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PotalType
{
    Broken,
    Money,
    Bone,
    Weapon,
    MiddleBoss,
    Boss,
    Shop,
}
public class Potal : InteractionObject
{
    [SerializeField] public PotalType type;
    public override void Interaction(Player player)
    {
        if (type == PotalType.Broken)
            return;
        if (GameManager.instance.isClear == false)
            return;
        base.Interaction(player);
        GameManager.instance.GoNextMap();
        switch (type)
        {
            case PotalType.Broken:
                break;
            case PotalType.Money:
                GameManager.instance.mapReward = MapReward.Coin;
                break;
            case PotalType.Bone:
                GameManager.instance.mapReward = MapReward.Bone;
                break;
            case PotalType.Weapon:
                GameManager.instance.mapReward = MapReward.Weapon;
                break;
            case PotalType.MiddleBoss:
                break;
            case PotalType.Boss:
                break;
            case PotalType.Shop:
                break;
        }

        Debug.Log("Go Next Map");
    }

    public override void SeeDetails(Player player)
    {
        if (type == PotalType.Broken)
            return;
        base.SeeDetails(player);
    }
}
