using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Skul.Movement;
using System;
using Skul.FSM;

public enum AttackType
{
    Physical,
    Magical
}

namespace Skul.Character
{
    public abstract class Character : MonoBehaviour,IHp,IPausable
    {
        //À¯´ÖÀÇ ½ºÅÈµé
        [Header("Status")]
        public float moveSpeed = 3.0f;
        public float dashForce = 3.0f;
        public float jumpForce = 3.0f;
        public int jumpCount = 0;
        public int maxJumpCount = 0;
        public int dashCount = 0;
        public int maxDashCount = 0;


        [SerializeField]protected Skul.Movement.Movement movement;
        protected Skul.FSM.StateMachine stateMachine;
     

        public float hp
        {
            get => _hp;
            set
            {
                if (_hp == value)
                    return;

                float prev = _hp;
                _hp = value;
                onHpChanged?.Invoke(value);
                if (prev > value)
                {
                    onHpDecreased?.Invoke(prev - value);
                    if (value <= _hpMin)
                    {
                        onHpMin?.Invoke();
                        stateMachine.ChangeState(StateType.Die);
                    }
                    else
                    {
                        if(stateMachine.states.ContainsKey(StateType.Hurt))
                            stateMachine.ChangeState(StateType.Hurt);
                    }
                }
                else
                {
                    onHpIncreased?.Invoke(value - prev);
                    if (value >= _hpMax)
                    {
                        onHpMax?.Invoke();
                    }
                }
            }
        }

        [Header("Status/Health")]
        [SerializeField] private float _hp;
        [SerializeField] private float _takenDamage;
        public float hpMax 
        { 
            get => _hpMax;
            set
            {
                float dev = value - _hpMax;
                if (dev == 0)
                    return;
                else if (dev < 0)
                {
                    if (value > _hp)
                        _hp = value;
                }
                else if (dev > 0)
                {
                    hp += dev;
                    _hpMax = value;
                }
                onHpMaxChanged?.Invoke(value);
                
            }
        }

        public float hpMin => _hpMin;

        public float TakenDamage
        {
            get => _takenDamage;
            set
            {
                if (_takenDamage == value)
                    return;
                _takenDamage = value;
                OnTakenDamageChanged?.Invoke(value);
            }
        }
        public event Action<float> OnTakenDamageChanged;

        private float _hpMin;
        [SerializeField] private float _hpMax;
        private float _mp;
        private float _mpMin;
        [SerializeField] private float _mpMax;

        public event Action<float> onHpChanged;

        public event Action<float> onHpIncreased;
        public event Action<float> onHpDecreased;
        public event Action<float> onHpMaxChanged;

        public event Action onHpMin;
        public event Action onHpMax;

        [SerializeField] protected float attackForce;

        public void Damage(GameObject damager, float amount,out float realDamage)
        {
            realDamage = amount * _takenDamage;
            hp -= realDamage;
        }

        public void Heal(GameObject healer, float amount)
        {
            hp += amount;
        }

        protected virtual void Start()
        {
            hp = hpMax;
        }

        protected virtual void Awake()
        {
            movement =GetComponentInParent<Skul.Movement.Movement>();
            stateMachine=GetComponentInParent<Skul.FSM.StateMachine>();

            movement.onHorizontalChanged += (value) =>
            {
                stateMachine.ChangeState(value == 0.0f ? FSM.StateType.Idle : FSM.StateType.Move);
            };
            
        }
        public void Pause(bool pause)
        {
            bool enable = pause == false;
            enabled = enable;
            stateMachine.enabled = enable;
            movement.enabled = enable;
        }
    }
}