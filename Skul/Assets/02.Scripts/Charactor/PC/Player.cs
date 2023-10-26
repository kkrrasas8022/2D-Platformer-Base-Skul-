using Skul.InputSystem;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Skul.Character.PC
{
    public class player:Character
    {
        private PlayerInput playerInput;
        public bool canDownJump;
        private GroundDetecter _detecter;
       
        
        protected override void Awake()
        {
            base.Awake();
            movement.direction = 1;
            _detecter = GetComponent<GroundDetecter>();
            movement.onDirectionChanged += (value) =>
            {
                if(value<0)
                {
                    _detecter.pos.x = -0.15f;
                }
                if(value>0)
                {
                    _detecter.pos.x = 0.15f;
                }
            };
            InputManager.Map map = new InputManager.Map();
            map.AddRawAxisAction("Horizontal", (value) =>
            {
                movement.horizontal = value;
                if (value > 0)
                    movement.direction = Movement.Movement.DIRECTION_RIGHT;
                else if (value < 0)
                    movement.direction = Movement.Movement.DIRECTION_LEFT;
            });
            //map.AddKeyDownAction(KeyCode.Space, () => stateMachine.ChangeState(stateMachine.currentType == FSM.StateType.Crouch ? FSM.StateType.DownJump : FSM.StateType.Jump));
            map.AddKeyDownAction(KeyCode.C, () => stateMachine.ChangeState(canDownJump==true?FSM.StateType.DownJump:FSM.StateType.Jump));
            map.AddKeyPressAction(KeyCode.DownArrow, () =>canDownJump = true);
            map.AddKeyUpAction(KeyCode.DownArrow, () =>canDownJump = false);
            //map.AddKeyUpAction(KeyCode.DownArrow, () => stateMachine.ChangeState(StateType.StandUp));
            map.AddKeyDownAction(KeyCode.X, () => stateMachine.ChangeState(
                _detecter.isDetected==false?FSM.StateType.JumpAttack:FSM.StateType.Attack));
            map.AddKeyDownAction(KeyCode.Z, () => stateMachine.ChangeState(FSM.StateType.Dash));
            map.AddKeyDownAction(KeyCode.A, () => stateMachine.ChangeState(FSM.StateType.Skill_1));
            map.AddKeyDownAction(KeyCode.S, () => stateMachine.ChangeState(FSM.StateType.Skill_2));
            map.AddKeyDownAction(KeyCode.Space, () => stateMachine.ChangeState(FSM.StateType.Switch));
            InputManager.instance.AddMap("PlayerAction", map);


        }
    }
}