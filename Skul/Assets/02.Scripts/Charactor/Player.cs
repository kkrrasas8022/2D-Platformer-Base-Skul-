using Skul.InputSystem;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Skul.Character
{
    public class player:Character
    {
        private PlayerInput playerInput;

        protected override void Awake()
        {
            base.Awake();

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
            map.AddKeyDownAction(KeyCode.Space, () => stateMachine.ChangeState(FSM.StateType.Jump));
            map.AddKeyDownAction(KeyCode.DownArrow, () =>
            {
                //if (stateMachine.ChangeState(StateType.LadderDown)) { }
                //else if (stateMachine.ChangeState(StateType.Crouch)) { }
            });
            //map.AddKeyUpAction(KeyCode.DownArrow, () => stateMachine.ChangeState(StateType.StandUp));
            map.AddKeyPressAction(KeyCode.A, () => stateMachine.ChangeState(FSM.StateType.Attack));
            InputManager.instance.AddMap("PlayerAction", map);


        }
    }
}