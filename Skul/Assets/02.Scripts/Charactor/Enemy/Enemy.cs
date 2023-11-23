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
            onHpMin += () => GameManager.GameManager.instance.EnemyDie?.Invoke();
        }
        private void Update()
        {
            if(hp<hpMax)
                _hpBar.gameObject.SetActive(true);
            _hpBar.GetComponent<RectTransform>().eulerAngles = (movement.direction == -1 ? new Vector3(0, 180,0):Vector3.zero);
        }
    }
}