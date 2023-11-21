using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;


namespace Skul.Character.Enemy {
    public class EnemyArrow : EnemyProjectile
    {
        public override void SetUp(GameObject owner, float damage, LayerMask target, Vector2 velocity)
        {
            base.SetUp(owner, damage, target, velocity);    
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
        }

        protected override void Update()
        {
            base.Update();
        }
    }
}