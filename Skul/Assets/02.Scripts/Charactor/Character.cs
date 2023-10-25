using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Skul.Movement;

namespace Skul.Character
{
    //���ֵ��� ���̽��� �Ǵ� character Ŭ����
    public abstract class Character : MonoBehaviour
    {
        //������ ���ȵ�
        [Header("State")]
        public float moveSpeed = 3.0f;
        public float dashForce = 3.0f;
        public float jumpForce = 3.0f;
        public int JumpCount=0;
        public int MaxJumpCount = 0;
        public int DashCount = 0;
        public int MaxDashCount = 0;

        protected Skul.Movement.Movement movement;
        protected Skul.FSM.StateMachine stateMachine;

        protected virtual void Awake()
        {
            movement=GetComponent<Skul.Movement.Movement>();
            stateMachine=GetComponent<Skul.FSM.StateMachine>();

            movement.onHorizontalChanged += (value) =>
            {
                stateMachine.ChangeState(value == 0.0f ? FSM.StateType.Idle : FSM.StateType.Move);
            };
        }
    }
}