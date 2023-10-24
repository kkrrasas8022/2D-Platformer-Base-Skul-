using System;
using System.Collections.Generic;
using UnityEngine;

namespace Skul.Character
{
    //객체의 바닥과의 접촉여부를 판정하는 클래스
    public class GroundDetecter:MonoBehaviour
    {
        [SerializeField] private Vector3 size;
        [SerializeField] private Vector3 pos;
        public bool isDetected =>detected;
        public Collider2D detected
        {
            get
            {
                return _detected;
            }
            private set
            {
                if (detected == value)
                    return;

                _isDetected = value;
                _detected = value;
            }
        }
        [SerializeField] private bool _isDetected;
        [SerializeField] private Collider2D _detected;

        [SerializeField] private LayerMask groundDe;
        private player player;

        private void Awake()
        {
            player = GetComponent<player>();
        }

        private void FixedUpdate()
        {
            detected = Physics2D.OverlapBox((Vector2)(transform.position - pos),
                                                         (Vector2)size,
                                                         0.0f,
                                                         groundDe);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(transform.position-pos, size);
        }
    }
}