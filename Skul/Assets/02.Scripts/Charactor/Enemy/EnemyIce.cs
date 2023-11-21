using UnityEngine;

namespace Skul.Character.Enemy
{
    public class EnemyIce:EnemyProjectile
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