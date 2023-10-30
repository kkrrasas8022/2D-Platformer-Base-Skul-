using System;
using System.Collections.Generic;
using UnityEngine;
using Skul.Character.PC;

namespace Skul.Character
{
    //객체의 바닥과의 접촉여부를 판정하는 클래스
    public class GroundDetecter:MonoBehaviour
    {
        [SerializeField] private Vector2 size;
        [SerializeField] public Vector2 pos;
        [SerializeField]private Movement.Movement _movement;
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
        private Player player;

        private void Awake()
        {
            player = GetComponent<Player>();
            _movement = GetComponent<Movement.Movement>();
        }

        private void FixedUpdate()
        {
            detected = Physics2D.OverlapBox((Vector2)transform.position - new Vector2(pos.x*_movement.direction,pos.y),
                                                         size,
                                                         0.0f,
                                                         groundDe);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(transform.position- (Vector3)new Vector2(pos.x * _movement.direction, pos.y), size);
        }
    }
}