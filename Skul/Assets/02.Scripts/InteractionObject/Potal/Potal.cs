using Skul.Character.PC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potal : InteractionObject
{
    public override void Interaction(Player player)
    {
        base.Interaction(player);
        Debug.Log("Go Next Map");
    }

    public override void SeeDetails(Player player)
    {
        base.SeeDetails(player);
    }
}
