using Skul.FSM.States;
using Skul.FSM;
using Skul.InputSystem;
using System;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.VisualScripting;
using UnityEngine.InputSystem.XR;
using Unity.VisualScripting.Antlr3.Runtime.Misc;

namespace Skul.Character.PC
{
    public class Player:Character
    {
        public bool canDownJump;
        private GroundDetecter _detecter;
        public float attackForce;
        [SerializeField] private GameObject _currentRen;
        [SerializeField] private List<GameObject> _renderers;
        [SerializeField] private List<SkulData> _skulDatas;
        //저장되어있는 스컬
        [SerializeField]public SkulData saveData;
        //현재 사용되는 스컬
        [SerializeField]public SkulData currentData;
        public Action OnSwitch;
        
        protected override void Awake()
        {
            //_skulDatas=new List<SkulData>();
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
            map.AddKeyDownAction(KeyCode.Space, ()=>Switch());
            map.AddKeyDownAction(KeyCode.F, () => 
            { 
                Debug.Log("KeyDown F");
            });
            map.AddKeyDownAction(KeyCode.Tab, () => 
            {
                Debug.Log("KeyDown Tab");
            });
            map.AddKeyDownAction(KeyCode.Escape, () =>
            {
                Debug.Log("KeyDown ESC");
            });
            map.AddKeyDownAction(KeyCode.D, () =>
            {
                Debug.Log("KeyDown D");
            });
            InputManager.instance.AddMap("PlayerAction", map);
        }
        protected override void Start()
        {
            base.Start();
            stateMachine.InitStates(new Dictionary<StateType, IStateEnumerator<StateType>>()
            {
                { StateType.Idle,       new StateIdle(stateMachine) },
                { StateType.Move,       new StateMove(stateMachine) },
                { StateType.Attack,     new StateAttack(stateMachine) },
                { StateType.Dash,       new StateDash(stateMachine) },
                { StateType.Jump,       new StateJump(stateMachine) },
                { StateType.DownJump,   new StateDownJump(stateMachine) },
                { StateType.Fall,       new StateFall(stateMachine) },
                { StateType.Hurt,       new StateHurt(stateMachine) },
                { StateType.Skill_1,    new StateSkill_1(stateMachine) },
                { StateType.Skill_2,    new StateSkill_2(stateMachine) },
                { StateType.Die,        new StateDie(stateMachine) },
                { StateType.JumpAttack, new StateJumpAttack(stateMachine) },
                { StateType.Switch,     new StateSwitch(stateMachine) },
            });
        }

        public void Switch()
        {
            _currentRen.SetActive(false);
            _currentRen = (_currentRen == _renderers[0] ? _renderers[1] : _renderers[0]);
            _currentRen.SetActive(true);

            SkulData tmpData;
            tmpData = currentData;
            currentData = saveData;
            saveData = tmpData;

            stateMachine.OnAnimatorChanged?.Invoke();
            stateMachine.ChangeState(StateType.Switch);
            
        }
    }
}