using System;
using System.Linq;
using UnityEngine;

namespace Skul.Character.PC
{
    public class NoramlHead:MonoBehaviour
    {
        [SerializeField]private GameObject _owner;
        [SerializeField] private bool _isStop;
        private CapsuleCollider2D _col;
        private CapsuleCollider2D _tri;
        private Rigidbody2D _rigid;
        private Vector2 _velocity;
        private float _damage;
        private LayerMask _targetMask;
        [SerializeField] private float _stopTime;
        [SerializeField] private float _stopMaxTime=1.0f;

        private void Awake()
        {
            _col = GetComponentsInChildren<CapsuleCollider2D>().Where(c => c.isTrigger == false).First();
            _tri = GetComponents<CapsuleCollider2D>().Where(c => c.isTrigger == true).First();
            _rigid=GetComponent<Rigidbody2D>();
            _rigid.gravityScale = 0;
            _stopTime = 0;
            _isStop = false;
        }

        public void SetUp(GameObject owner,Vector2 velocity,float damage,LayerMask target)
        {
            _owner = owner;
            _velocity = velocity;
            _damage = damage;
            _targetMask = target;
        }

        private void Update()
        {
            _owner.GetComponent<Normal>()._headsPos = transform.position;

            if (_isStop == false)
            {
                if (_stopTime >= _stopMaxTime)
                {
                    StopSystem();
                }
                _stopTime += Time.deltaTime;
            }
        }

        private void FixedUpdate()
        {
            if( _isStop == false) 
                 transform.position += (Vector3)_velocity * Time.fixedDeltaTime;
        }
        private void StopSystem()
        {
            _isStop=true;
            _rigid.velocity = _velocity;
            _rigid.gravityScale = 1;
            _tri.enabled = false;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if ((1 << collision.gameObject.layer & _targetMask) > 0)
            {
                if (collision.TryGetComponent(out IHp ihp))
                {
                    ihp.Damage(_owner, _damage);
                }
                StopSystem();
            }
        }
    }
}