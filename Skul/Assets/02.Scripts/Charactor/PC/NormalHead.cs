using System;
using System.Linq;
using UnityEngine;

namespace Skul.Character.PC
{
    public class NoramlHead:PlayerProjectile
    {
        [SerializeField] private bool _isStop;
        [SerializeField] private float _stopTime;
        [SerializeField] private float _stopMaxTime = 1.0f;

        protected override void Awake()
        {
            base.Awake();
            _isStop = false;
            _stopTime = 0.0f;
        }

        public override void SetUp(GameObject owner, Vector2 velocity, float damage, LayerMask target)
        {
            base.SetUp(owner, velocity, damage, target);
        }

        protected override void FixedUpdate()
        {
            //base.FixedUpdate();
            if (_isStop == false)
                base.FixedUpdate();
        }

        protected override void Update()
        {
            base.Update();
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
        private void StopSystem()
        {
            _rigid.velocity = _velocity;
            _rigid.gravityScale = 1;
            _tri.enabled = false;
            _isStop = true;
        }
        
        protected override void OnTriggerEnter2D(Collider2D collision)
        {
            base.OnTriggerEnter2D(collision);
            if(_istected==true)
            {
                StopSystem();
            }
        }
    }
}