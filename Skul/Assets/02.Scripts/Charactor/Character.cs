using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Skul.Movement;

namespace Skul.Character
{
    public abstract class Character : MonoBehaviour
    {
        [Header("State")]
        public float moveSpeed = 3.0f;
        public float dashForce = 3.0f;
        public float jumpForce = 3.0f;

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