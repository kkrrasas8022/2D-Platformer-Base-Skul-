using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;


namespace Skul.Character.Enemy {
    public class EnemyProjectile : MonoBehaviour
    {
        private GameObject _owner;
        private Vector2 _velocity;
        private float _damage;
        private LayerMask _targetMask;
        [SerializeField]private float _aliveTime;
        [SerializeField]private float _aliveTimeMax=10.0f;

        public void SetUp(GameObject owner, float damage, LayerMask target, Vector2 velocity)
        {
            _owner = owner;
            _velocity = velocity;
            _damage = damage;
            _targetMask = target;
        }

        private void FixedUpdate()
        {
            transform.position += (Vector3)_velocity * Time.fixedDeltaTime;
        }
        private void Update()
        {
            _aliveTime += Time.deltaTime;
            if( _aliveTime >= _aliveTimeMax )
            {
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if ((1 << collision.gameObject.layer & _targetMask) > 0)
            {
                if (collision.TryGetComponent(out IHp ihp))
                {
                    ihp.Damage(_owner, _damage);
                    Destroy(gameObject);
                }
            }
        }
    }
}