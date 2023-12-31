using Skul.Character;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Skul.Movement
{
    //객체의 이동을 담당하는 추상 클래스
    public abstract class Movement : MonoBehaviour
    {
        public bool isMovable;
        public bool isDirectionChangeable;
        public const int DIRECTION_RIGHT = 1;
        public const int DIRECTION_LEFT = -1;
        [SerializeField] protected EdgeDetecter _edgeDetecter;

        public bool changedir;
        public int direction
        {
            get => _direction;
            set
            {
                if (isDirectionChangeable == false)
                    return;
                if (_direction == value) 
                    return;
                if (value < 0)
                {
                    if (_edgeDetecter&&_edgeDetecter._edgeEnd)
                        transform.position -=new Vector3(0.6f,0);
                    transform.eulerAngles = new Vector3(0.0f, 180.0f, 0.0f);
                    _direction = DIRECTION_LEFT;
                }
                else
                {
                    if (_edgeDetecter&&_edgeDetecter._edgeEnd)
                        transform.position += new Vector3(0.6f, 0);
                    transform.eulerAngles = Vector3.zero;
                    _direction = DIRECTION_RIGHT;
                }
                onDirectionChanged?.Invoke(_direction);
            }
        }
        [SerializeField]private int _direction;
        public event Action<int> onDirectionChanged;

        public float horizontal
        {
            get => _horizontal;
            set
            {
                //이동불가일경우 변수 변경 x
                if (isMovable == false) return;
                if (_horizontal == value) return;
                _horizontal = value;
                onHorizontalChanged?.Invoke(value);
            }
        }
        [SerializeField]private float _horizontal;
        public event Action<float> onHorizontalChanged;

        private Rigidbody2D _rigid;
        private Vector2 _move;
        [SerializeField] private float _speed = 3.0f;
        

        private void Awake()
        {
            _rigid=GetComponent<Rigidbody2D>();
            GameElement.GameManager.instance.player.OnMoveSpeedChanged += (value) =>
            {
                _speed *= value;
            };
        }

        protected void Update()
        {
            if (isMovable)
                _move = new Vector2(horizontal, 0);
            else
                _move = Vector2.zero;
        }

        private void FixedUpdate()
        {
            _rigid.position += _move * _speed * Time.fixedDeltaTime;
        }
    }
}