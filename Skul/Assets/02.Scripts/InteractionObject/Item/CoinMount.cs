using Skul.Character.PC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinMount : InteractionObject
{
    [SerializeField] private int _coinAmount;
    public override void Interaction(Player player)
    {
        base.Interaction(player);
        player.curCoin += _coinAmount;
        Destroy(gameObject);
    }
}
