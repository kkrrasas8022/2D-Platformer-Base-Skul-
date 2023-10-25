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
       
        
        protected override void Awake()
        {
            base.Awake();
            movement.direction = 1;
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
            map.AddKeyDownAction(KeyCode.DownArrow, () =>canDownJump = true);
            map.AddKeyUpAction(KeyCode.DownArrow, () =>canDownJump = false);
            //map.AddKeyUpAction(KeyCode.DownArrow, () => stateMachine.ChangeState(StateType.StandUp));
            map.AddKeyPressAction(KeyCode.X, () => stateMachine.ChangeState(FSM.StateType.Attack));
            map.AddKeyPressAction(KeyCode.Z, () => stateMachine.ChangeState(FSM.StateType.Dash));
            map.AddKeyPressAction(KeyCode.A, () => stateMachine.ChangeState(FSM.StateType.Skill_1));
            map.AddKeyPressAction(KeyCode.S, () => stateMachine.ChangeState(FSM.StateType.Skill_2));
            InputManager.instance.AddMap("PlayerAction", map);


        }
    }
}