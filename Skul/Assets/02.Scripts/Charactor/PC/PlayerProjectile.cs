using Skul.Movement;
using System.Linq;
using UnityEngine;

namespace Skul.Character.PC
{
    public abstract class PlayerProjectile : MonoBehaviour,IPausable
    {
        [SerializeField] protected GameObject _owner;
        [SerializeField] protected Collider2D _tri;
        [SerializeField] protected Rigidbody2D _rigid;
        [SerializeField] protected Vector2 _velocity;
        [SerializeField] protected float _damage;
        [SerializeField] protected LayerMask _targetMask;
        [SerializeField] protected bool _istected;

        [SerializeField] protected float _destroyTime;
        [SerializeField] protected float _destroyMaxTime;

        protected virtual void Awake()
        {
            _tri = (_tri==null?GetComponents<Collider2D>().Where(c => c.isTrigger == true).First():_tri);
            _rigid = GetComponent<Rigidbody2D>();
            _rigid.gravityScale = 0;
            _destroyTime = 0;
        }

        public virtual void SetUp(GameObject owner, Vector2 velocity, float damage, LayerMask target)
        {
            _owner = owner;
            _velocity = velocity;
            _damage = damage;
            _targetMask = target;
        }

        protected virtual void Update()
        {
            if (_destroyTime > _destroyMaxTime)
                Destroy(gameObject);
            _destroyTime += Time.deltaTime;
        }

        protected virtual void FixedUpdate()
        {
            transform.position += (Vector3)_velocity * Time.fixedDeltaTime;
        }
        

        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
            if ((1 << collision.gameObject.layer & _targetMask) > 0)
            {
                if (collision.TryGetComponent(out IHp ihp))
                {
                    ihp.Damage(_owner, _damage, out float damage);
                }
                _istected = true;
            }
        }

        public void Pause(bool pause)
        {
            bool enable = pause == false;
            enabled = enable;
        }
    }
}