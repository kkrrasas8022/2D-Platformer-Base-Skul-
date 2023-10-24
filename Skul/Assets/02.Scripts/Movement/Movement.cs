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
        //이동가능한 객체인지 확인해주는 변수
        public bool isMovable;
        public bool isDirectionChangeable;
        //방향을 전역변수로 설정
        public const int DIRECTION_RIGHT = 1;
        public const int DIRECTION_LEFT = -1;

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
                    transform.eulerAngles = new Vector3(0.0f, 180.0f, 0.0f);
                    _direction = DIRECTION_LEFT;
                }
                else
                {
                    transform.eulerAngles = Vector3.zero;
                    _direction = DIRECTION_RIGHT;
                }
                onDirectionChanged?.Invoke(_direction);
            }
        }
        private int _direction;
        //방향이 바뀌었을 때 호출되는 이벤트 대리자
        public event Action<int> onDirectionChanged;

        public float horizontal
        {
            get => _horizontal;
            set
            {
                Debug.Log("horizontal set");
                //이동불가일경우 변수 변경 x
                if (isMovable == false) return;
                if (_horizontal == value) return;
                _horizontal = value;
                onHorizontalChanged?.Invoke(value);
            }
        }
        //좌우 이동값을 가지는 변수
        [SerializeField]private float _horizontal;
        //이동값이 변했을때 호출되는 이벤트 대리자
        public event Action<float> onHorizontalChanged;

        //----실질적인 이동을 하는 구간----
        //이동을 구현하기 위해 rigidbody를 가져옴
        private Rigidbody2D _rigid;
        //이동방향을 나타냄
        private Vector2 _move;
        //이동속도
        [SerializeField] private float _speed = 1.0f;

        private void Awake()
        {
            _rigid=GetComponent<Rigidbody2D>();
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