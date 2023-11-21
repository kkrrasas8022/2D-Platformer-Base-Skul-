using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Skul.UI;
using Unity.VisualScripting;

namespace Skul.Character.Enemy {
    public class Enemy :Character
    {
        [SerializeField]private EnemyHp _hpBar;
        [SerializeField]protected LayerMask _targetMask;
        protected Rigidbody2D rigid;

        protected override void Awake()
        {
            base.Awake();
            rigid = GetComponent<Rigidbody2D>();
        }
        private void Update()
        {
            if(hp<hpMax)
                _hpBar.gameObject.SetActive(true);
        }
    }
}