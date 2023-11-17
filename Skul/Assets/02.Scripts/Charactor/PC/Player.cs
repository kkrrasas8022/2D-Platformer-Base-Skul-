using Skul.FSM.States;
using Skul.UI;
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
using Skul.Data;
using Skul.Item;

public enum StatusType
{
    MaxHp,
    TakenDamage,
    Physical,
    Magical,
    AttackSpeed,
    MoveSpeed,
    ConsentSpeed,
    SkillCoolDown,
    SwitchCoolDown,
    EssenceCoolDown,
    CriticalPersent,
    CriticalDamage,
}

namespace Skul.Character.PC
{
    public class Player:Character
    {
        public PlayerInventory inventory;

        [Header("UI")]
        [SerializeField] private InventoryUI _inventoryUI;


        public float AttackForce
        {
            get 
            {
                if (attackTypes == AttackType.Physical)
                    return attackForce * _physical;
                return attackForce * _magical;
            }
            
        }
        public int curCoin
        {
            get=>_curCoin;
            set
            {
                _curCoin = value;
                OnCoinChanged?.Invoke(value);

            }
        }
        public event Action<int> OnCoinChanged;
        public event Action<int> OnBoneChanged;
        public int curBone
        {
            get => _curBone;
            set
            {
                _curBone = value;
                OnBoneChanged?.Invoke(value);

            }
        }

        [SerializeField]private int _curCoin;
        [SerializeField]private int _curBone;

        public bool canInteraction;
        [SerializeField] private List<InteractionObject> interactionObjectsList;
        public InteractionObject canInteractionObject;

        public bool canDownJump;
        private GroundDetecter _detecter;
        [SerializeField] public PlayerAttacks _currentRen;
        [SerializeField] public List<PlayerAttacks> _renderers;
       
        public Action OnSwitch;
        public Movement.Movement playerMovement { get=>movement; }
        public AttackType attackTypes;

        [Header("Status/Power")]
        [SerializeField]private float _physical;
        [SerializeField]private float _magical;
        [SerializeField] private float _physicPower;
        [SerializeField] private float _magicPower;
        [Header("Status/Speed")]
        [SerializeField] private float _attackSpeed;
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _consentSpeed;
        [Header("Status/CoolDown")]
        [SerializeField] private float _skillCoolDown;
        [SerializeField] private float _switchCoolDown;
        [SerializeField] private float _essenceCoolDown;
        [Header("Status/Critical")]
        [SerializeField] private float _criticalPersent;
        [SerializeField] private float _criticalDamage;

        public event Action<float> OnPhysicChanged;
        public event Action<float> OnMagicChanged;
        public event Action<float> OnAttackSpeedChanged;
        public event Action<float> OnMoveSpeedChanged;
        public event Action<float> OnConsentSpeedChanged;
        public event Action<float> OnSkillCoolDownChanged;
        public event Action<float> OnSwitchCoolDownChanged;
        public event Action<float> OnEssenceCoolDownChanged;
        public event Action<float> OnCriticalPersentChanged;
        public event Action<float> OnCriticalDamageChanged;

        public float PhysicPower
        {
            get=> _physicPower;
            set
            {
                if (_physicPower==value) return;
                _physicPower = value;
                OnPhysicChanged?.Invoke(value);
            }
        }
        public float MagicPower
        {
            get => _magicPower;
            set
            {
                if (_magicPower == value) return;
                _magicPower = value;
                OnMagicChanged?.Invoke(value);
            }
        }
        public float AttackSpeed
        {
            get => _attackSpeed;
            set
            {
                if (_attackSpeed == value) return;
                _attackSpeed = value;
                OnAttackSpeedChanged?.Invoke(value);
            }
        }
        public float MoveSpeed
        {
            get => _moveSpeed;
            set
            {
                if (_moveSpeed == value) return;
                _moveSpeed = value;
                OnMoveSpeedChanged?.Invoke(value);
            }
        }
        public float ConsentSpeed
        {
            get => _consentSpeed;
            set
            {
                if (_consentSpeed == value) return;
                _consentSpeed = value;
                OnConsentSpeedChanged?.Invoke(value);
            }
        }
        public float SkillCoolDown
        {
            get => _skillCoolDown;
            set
            {
                if (_skillCoolDown == value) return;
                _skillCoolDown = value;
                OnSkillCoolDownChanged?.Invoke(value);
            }
        }
        public float SwitchCoolDown
        {
            get => _switchCoolDown;
            set
            {
                if (_switchCoolDown == value) return;
                _switchCoolDown = value;
                OnSwitchCoolDownChanged?.Invoke(value);
            }
        }
        public float EssenceCoolDown
        {
            get => _essenceCoolDown;
            set
            {
                if (_essenceCoolDown == value) return;
                _essenceCoolDown = value;
                OnEssenceCoolDownChanged?.Invoke(value);
            }
        }
        public float CriticalPersent
        {
            get => _criticalPersent;
            set
            {
                if (_criticalPersent == value) return;
                _criticalPersent = value;
                OnCriticalPersentChanged?.Invoke(value);
            }
        }
        public float CriticalDamage
        {
            get => _criticalDamage;
            set
            {
                if (_criticalDamage == value) return;
                _criticalDamage = value;
                OnCriticalDamageChanged?.Invoke(value);
            }
        }

        protected override void Awake()
        {
            base.Awake();
            inventory = GetComponent<PlayerInventory>();
            interactionObjectsList = new List<InteractionObject>();
            /*
            OnChangeItem += (value) =>
            {
                switch (value.power.type)
                {
                    case StatusType.Physical:
                        PhysicPower += value.power.power;
                        break;
                    case StatusType.MaxHp:
                        break;
                    case StatusType.TakenDamage:
                        break;
                    case StatusType.Magical:
                        break;
                    case StatusType.AttackSpeed:
                        break;
                    case StatusType.MoveSpeed:
                        break;
                    case StatusType.ConsentSpeed:
                        break;
                    case StatusType.SkillCoolDown:
                        break;
                    case StatusType.SwitchCoolDown:
                        break;
                    case StatusType.EssenceCoolDown:
                        break;
                    case StatusType.CriticalPersent:
                        break;
                    case StatusType.CriticalDamage:
                        break;
                }
            };
            */

            _curCoin = 0;
            _curBone = 0;

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
            map.AddKeyPressAction(KeyCode.DownArrow, () => 
            {
                canDownJump = true;
                if (canInteraction)
                {
                    canInteractionObject.SeeDetails(this);
                }
            });
            map.AddKeyUpAction(KeyCode.DownArrow, () =>
            {
                canDownJump = false;
                if (canInteraction)
                {
                    canInteractionObject.ColseDetails(this);
                }
            });
            //map.AddKeyUpAction(KeyCode.DownArrow, () => stateMachine.ChangeState(StateType.StandUp));
            map.AddKeyDownAction(KeyCode.X, () => stateMachine.ChangeState(
                _detecter.isDetected==false?FSM.StateType.JumpAttack:FSM.StateType.Attack));
            map.AddKeyDownAction(KeyCode.Z, () => stateMachine.ChangeState(FSM.StateType.Dash));
            map.AddKeyDownAction(KeyCode.A, () => stateMachine.ChangeState(FSM.StateType.Skill_1));
            map.AddKeyDownAction(KeyCode.S, () => stateMachine.ChangeState(FSM.StateType.Skill_2));
            map.AddKeyDownAction(KeyCode.Space, ()=>Switch());
            map.AddKeyDownAction(KeyCode.D, () => 
            {
                if (inventory.EssenceData)
                    inventory.EssenceData.EssenceSkill();
            });
            map.AddKeyDownAction(KeyCode.F, () => 
            { 
                Debug.Log("KeyDown F");
                if (canInteraction)
                {
                    interactionObjectsList.Remove(canInteractionObject);
                    canInteractionObject.Interaction(this);
                }
            });
            map.AddKeyPressAction(KeyCode.F, () => { });
            map.AddKeyDownAction(KeyCode.Tab, () => 
            {
                _inventoryUI.Show();
            });
            map.AddKeyDownAction(KeyCode.Escape, () =>
            {
                Debug.Log("KeyDown ESC");
            });
            InputManager.instance.AddMap("PlayerAction", map);

            /*
            OnChangeItem += (value) =>
            {
                if(!haveEngrave.TryAdd(value.engraves[0],1))
                    haveEngrave[value.engraves[0]]++;
                if(!haveEngrave.TryAdd(value.engraves[1], 1))
                    haveEngrave[value.engraves[1]]++;
            };
            */
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
            if(_renderers.Count <2)
                return;
            OnSwitch?.Invoke();
            _currentRen.gameObject.SetActive(false);
            _currentRen = (_currentRen == _renderers[0] ? _renderers[1] : _renderers[0]);
            _currentRen.gameObject.SetActive(true);

            inventory.SwitchHead();

            AnimatorChange(_currentRen.GetComponent<Animator>());
            stateMachine.ChangeState(StateType.Switch);
        }

        public void AnimatorChange(Animator ani)
        {
            Debug.Log("»£√‚");
            stateMachine.OnAnimatorChanged?.Invoke(_currentRen.GetComponent<Animator>());
        }

        [SerializeField] private Animator ani;

        private void Update()
        {
        }



        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.CompareTag("InteractionObject"))
            {
                canInteraction = true;
                canInteractionObject = collision.gameObject.GetComponent<InteractionObject>();
                interactionObjectsList.Add(canInteractionObject);
            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("InteractionObject"))
            {
                interactionObjectsList.Remove(collision.gameObject.GetComponent<InteractionObject>());
                if(interactionObjectsList.Count<=0)
                {
                    canInteraction = false;
                    canInteractionObject = null;
                }
            }
        }

        
    }
}