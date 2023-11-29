using Skul.Character.PC;
using Skul.GameElement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaisButton : MonoBehaviour
{
    [SerializeField] private int count;

 
    public void OnButtonClick()
    {
        Debug.Log("click");
        if (count==0)
        {
            GameManager.instance.player.curCoin += 10;
        }
        else if(count == 1)
        {
            GameManager.instance.player.curBone += 10;
        }
    }
}
