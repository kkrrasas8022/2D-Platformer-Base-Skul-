using Skul.Character.Enemy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Skul.Movement;
using Unity.Mathematics;

namespace Skul.UI
{
    public class EnemyHp : MonoBehaviour
    {
        [SerializeField]private Enemy target;
        [SerializeField]private Slider hpBar;

        private void Start()
        {
            target = GetComponentInParent<Enemy>();
             hpBar = GetComponentInChildren<Slider>();

            hpBar.minValue = 0.0f;
            hpBar.maxValue = target.hpMax;
            hpBar.value = target.hp;

            target.onHpChanged += (value) =>
            {
                hpBar.value = value;
            };

            Movement.Movement movement = target.GetComponent<Movement.Movement>();
            movement.onDirectionChanged += (value) =>
            {
                GetComponent<RectTransform>().rotation = value > 0 ? new Quaternion(0, 0, 0, 0) : new Quaternion(0.0f, 180.0f, 0.0f,0);
            };
        }
        
    }

}