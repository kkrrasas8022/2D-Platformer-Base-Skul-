using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Skul.Movement
{
    //��ü�� �̵��� ����ϴ� �߻� Ŭ����
    public abstract class Movement : MonoBehaviour
    {
        //�̵������� ��ü���� Ȯ�����ִ� ����
        public bool isMovable;
        public bool isDirectionChangeable;
        //������ ���������� ����
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
        //������ �ٲ���� �� ȣ��Ǵ� �̺�Ʈ �븮��
        public event Action<int> onDirectionChanged;

        public float horizontal
        {
            get => _horizontal;
            set
            {
                Debug.Log("horizontal set");
                //�̵��Ұ��ϰ�� ���� ���� x
                if (isMovable == false) return;
                if (_horizontal == value) return;
                _horizontal = value;
                onHorizontalChanged?.Invoke(value);
            }
        }
        //�¿� �̵����� ������ ����
        [SerializeField]private float _horizontal;
        //�̵����� �������� ȣ��Ǵ� �̺�Ʈ �븮��
        public event Action<float> onHorizontalChanged;

        //----�������� �̵��� �ϴ� ����----
        //�̵��� �����ϱ� ���� rigidbody�� ������
        private Rigidbody2D _rigid;
        //�̵������� ��Ÿ��
        private Vector2 _move;
        //�̵��ӵ�
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