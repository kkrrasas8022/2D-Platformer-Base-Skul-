using Skul.Movement;
using UnityEngine;

namespace Skul.Character.Enemy
{
    public class EnemyProjectile : MonoBehaviour,IPausable
    {
        protected GameObject _owner;
        protected Vector2 _velocity;
        protected float _damage;
        protected LayerMask _targetMask;

        [SerializeField] protected float _aliveTime;
        [SerializeField] protected float _aliveTimeMax = 10.0f;

        public virtual void SetUp(GameObject owner, float damage, LayerMask target, Vector2 velocity)
        {
            _owner = owner;
            _velocity = velocity;
            _damage = damage;
            _targetMask = target;
        }
        protected virtual void FixedUpdate()
        {
            transform.position += (Vector3)_velocity * Time.fixedDeltaTime;
        }
        protected virtual void Update()
        {
            _aliveTime += Time.deltaTime;
            if (_aliveTime >= _aliveTimeMax)
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
                    ihp.Damage(_owner, _damage, out float damage);
                    Destroy(gameObject);
                }
            }
        }

        public void Pause(bool pause)
        {
            bool enable = pause == false;
            enabled = enable;
        }
    }
}