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
using UnityEditor.ShaderGraph.Drawing;
using Skul.Tools;

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

        [SerializeField] public int nowComboCount;
        [SerializeField] private bool _nowCombo;
        [SerializeField] private float _canComboTime;
        [SerializeField] private float _canComboMaxtime;

        [SerializeField] public float switchMaxCooltime;
        [SerializeField] public float switchCoolTime;
        [SerializeField] public float skill1CoolTime;
        [SerializeField] public float skill2CoolTime;
        [SerializeField] public bool canUseSwitch;
        [SerializeField] public bool canUseSkill1;
        [SerializeField] public bool canUseSkill2;

        [SerializeField] private float _realAttackForce;

        public float AttackForce
        {
            get 
            {
                if (attackTypes == AttackType.Physical)
                    return attackForce * _physicPower;
                else if (attackTypes == AttackType.Magical)
                    return attackForce * _magicPower;
                else
                    return attackForce;
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
        [SerializeField] public PlayerAttacks currentRen;
        [SerializeField] public PlayerAttacks subRen;
        [SerializeField] public List<PlayerAttacks> renderers;
        [SerializeField] public bool canCharge;
        public Action OnSwitch;
        public Movement.Movement playerMovement { get=>movement; }
        public AttackType attackTypes;

        [Header("Status/Power")]
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
            _curCoin = GameElement.GameManager.instance.startCoin;

            _realAttackForce = 0;

            inventory = GetComponent<PlayerInventory>();
            interactionObjectsList = new List<InteractionObject>();


            _curCoin = 0;
            _curBone = 0;

            movement.direction = 1;
            _detecter = GetComponent<GroundDetecter>();

            movement.onDirectionChanged += (value) =>
            {
                if (value < 0)
                {
                    _detecter.pos.x = -0.15f;
                }
                if (value > 0)
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
            map.AddKeyDownAction(KeyCode.C, () =>
            {
            stateMachine.ChangeState(canDownJump == true ? (_detecter.detected2? FSM.StateType.DownJump:stateMachine.currentType) : FSM.StateType.Jump);
            });
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
            map.AddKeyDownAction(KeyCode.X, () =>
            {
                _nowCombo = true;
                _canComboTime = 0;
                nowComboCount++;
                if (nowComboCount > inventory.CurHeadData.skulData.attackComboCount)
                    nowComboCount = 1;
                stateMachine.ChangeState(
                _detecter.isDetected == false ? FSM.StateType.JumpAttack : FSM.StateType.Attack);
            });
            map.AddKeyPressAction(KeyCode.X, () =>
            {
                if (_detecter.isDetected == true&&canCharge)
                {
                    stateMachine.ChangeState(StateType.Charging);
                }
            });
            map.AddKeyUpAction(KeyCode.X, () =>
            {
                if (_detecter.isDetected == true && canCharge)
                {
                    stateMachine.ChangeState(StateType.Attack);
                }
            });
            map.AddKeyDownAction(KeyCode.A, () =>
            {
                if (canUseSkill1)
                {
                    stateMachine.ChangeState(FSM.StateType.Skill_1); 
                    canUseSkill1= false;
                }
                else
                    return;
             });
            map.AddKeyPressAction(KeyCode.A, () =>
            {
                if (_detecter.isDetected == true && canCharge)
                {
                    stateMachine.ChangeState(StateType.Charging);
                }
            });
            map.AddKeyUpAction(KeyCode.A, () =>
            {
                if (_detecter.isDetected == true && canCharge)
                {
                    stateMachine.ChangeState(StateType.Skill_1);
                }
            });
            map.AddKeyDownAction(KeyCode.S, () =>
            {
                if (canUseSkill2)
                { 
                    stateMachine.ChangeState(FSM.StateType.Skill_2);
                    canUseSkill2= false;
                }
                else
                    return;
            });
            map.AddKeyPressAction(KeyCode.S, () =>
            {
                if (_detecter.isDetected == true && canCharge)
                {
                    stateMachine.ChangeState(StateType.Charging);
                }
            });
            map.AddKeyUpAction(KeyCode.S, () =>
            {
                if (_detecter.isDetected == true && canCharge)
                {
                    stateMachine.ChangeState(StateType.Skill_2);
                }
            });
            map.AddKeyDownAction(KeyCode.Z, () => stateMachine.ChangeState(FSM.StateType.Dash));
            map.AddKeyDownAction(KeyCode.Space, ()=>
            {
                if (canUseSwitch)
                { 
                    Switch(); 
                    canUseSwitch = false;
                }
            });
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
            map.AddKeyPressAction(KeyCode.F, () =>
            { 
                
            });
            map.AddKeyUpAction(KeyCode.F, () =>
            {
               
            });
            map.AddKeyDownAction(KeyCode.Tab, () => 
            {
                InventoryUI.instance.Show();
                PauseController.instance.OnPause?.Invoke();
            });
            map.AddKeyDownAction(KeyCode.Escape, () =>
            {
                Debug.Log("KeyDown ESC");
            });
            InputManager.instance.AddMap("PlayerAction", map);

            OnAttackSpeedChanged += (value) =>
            {
                ani.SetFloat("AttackSpeed", value);
            };

            

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
                { StateType.Skill_1,    new StateSkill_1(stateMachine) },
                { StateType.Skill_2,    new StateSkill_2(stateMachine) },
                { StateType.Die,        new StateDie(stateMachine) },
                { StateType.JumpAttack, new StateJumpAttack(stateMachine) },
                { StateType.Switch,     new StateSwitch(stateMachine) },
                {StateType.Charging, new StateCharging(stateMachine) }
            });
            
        }
        public void Switch()
        {
            if(renderers.Count <2)
                return;
            OnSwitch?.Invoke();
            currentRen.gameObject.SetActive(false);
            currentRen = (currentRen == renderers[0] ? renderers[1] : renderers[0]);
            subRen= (currentRen == renderers[0] ? renderers[1] : renderers[0]);
            currentRen.gameObject.SetActive(true);
          
            inventory.SwitchHead();

            AnimatorChange(currentRen.GetComponent<Animator>());
            stateMachine.ChangeState(StateType.Switch);
        }

        public void AnimatorChange(Animator ani)
        {
            Debug.Log("ȣ��");
            stateMachine.OnAnimatorChanged?.Invoke(currentRen.GetComponent<Animator>());
            this.ani = ani;
        }

        [SerializeField] private Animator ani;

        private void Update()
        {
            if(_nowCombo)
            {
                _canComboTime += Time.deltaTime;
                if(_canComboTime>_canComboMaxtime)
                {
                    _nowCombo= false;
                    nowComboCount = 0;
                    
                }
            }


            _realAttackForce = AttackForce;
            if (canUseSkill1 == false)
                skill1CoolTime += Time.deltaTime * _skillCoolDown;
            if (skill1CoolTime > ((ActiveSkillData)SkillManager.instance[currentRen.hadSkillsID[0]]).CoolTime)
            {
                canUseSkill1 = true;

                skill1CoolTime = 0.0f;
            }
            if (currentRen.hadSkillsID.Count > 1)
            {
                if (canUseSkill2 == false)
                    skill2CoolTime += Time.deltaTime * _skillCoolDown;
                if (skill2CoolTime > ((ActiveSkillData)SkillManager.instance[currentRen.hadSkillsID[1]]).CoolTime)
                {
                    canUseSkill2 = true;
                    skill2CoolTime = 0.0f;
                }
            }
            else
            {
                canUseSkill2 = false;
            }
            if (inventory.SaveHeadData == null)
                canUseSwitch = false;
            else
            {
                if (canUseSwitch == false)
                    switchCoolTime += Time.deltaTime * _switchCoolDown;
                if(switchCoolTime>switchMaxCooltime)
                {
                    canUseSwitch = true;
                    switchCoolTime = 0.0f;
                }
            }

        }



        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.CompareTag("InteractionObject"))
            {
                canInteraction = true;
                canInteractionObject = collision.gameObject.GetComponent<InteractionObject>();
                interactionObjectsList.Add(canInteractionObject);
                int max = 0;
                foreach(InteractionObject item in interactionObjectsList)
                {
                    if (item.sortingObject > max)
                        max = item.sortingObject;
                }
                foreach(InteractionObject item in interactionObjectsList)
                {
                    canInteractionObject = (item.sortingObject == max ? canInteractionObject : item);
                }
            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("InteractionObject"))
            {
                interactionObjectsList.Remove(collision.gameObject.GetComponent<InteractionObject>());
                int max = 0;
                foreach (InteractionObject item in interactionObjectsList)
                {
                    if (item.sortingObject > max)
                        max = item.sortingObject;
                }
                foreach (InteractionObject item in interactionObjectsList)
                {
                    canInteractionObject = (item.sortingObject == max ? canInteractionObject : item);
                }
                if (interactionObjectsList.Count<=0)
                {
                    canInteraction = false;
                    canInteractionObject = null;
                }
            }
        }
        
        
    }
}