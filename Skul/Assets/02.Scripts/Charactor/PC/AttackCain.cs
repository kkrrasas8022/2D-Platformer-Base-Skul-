using System;
using System.Linq;
using UnityEngine;

namespace Skul.Character.PC
{
    public class AttackCain : PlayerProjectile
    {
        protected override void Awake()
        {
            base.Awake();
            GetComponent<Animator>().Play("MoveAttack");
        }
        public override void SetUp(GameObject owner, Vector2 velocity, float damage, LayerMask target)
        {
            base.SetUp(owner, velocity, damage, target);
        }

        protected override void Update()
        {
            base.Update();
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();

        }


        protected override void OnTriggerEnter2D(Collider2D collision)
        {
            base.OnTriggerEnter2D(collision);
        }
    }
}