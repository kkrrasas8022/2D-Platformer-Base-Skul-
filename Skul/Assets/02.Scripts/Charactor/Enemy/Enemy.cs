using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Skul.UI;

namespace Skul.Character.Enemy {
    public class Enemy :Character
    {
        [SerializeField]private EnemyHp _hpBar;
        private void Update()
        {
            if(hp<hpMax)
                _hpBar.gameObject.SetActive(true);
        }
    }
}