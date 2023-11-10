using Skul.Character.PC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class COinbutton : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private int count;
    public void OnButtonClick()
    {
        if (count==0)
        {
            _player.curCoin += 10;
        }
        else if(count == 1)
        {
            _player.curBone += 10;
        }




    }
}
