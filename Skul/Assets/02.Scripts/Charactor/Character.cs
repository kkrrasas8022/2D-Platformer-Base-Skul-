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
    //유닛들의 베이스가 되는 character 클래스
    public abstract class Character : MonoBehaviour,IHp,IMp
    {
        //유닛의 스탯들
        [Header("State")]
        public float moveSpeed = 3.0f;
        public float dashForce = 3.0f;
        public float jumpForce = 3.0f;
        public int jumpCount = 0;
        public int maxJumpCount = 0;
        public int dashCount = 0;
        public int maxDashCount = 0;

        [SerializeField]protected float attackForce;

        protected Skul.Movement.Movement movement;
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
        public float hpMax => _hpMax;

        public float hpMin => _hpMin;

        public float mp { get; set; }

        public float mpMax => _mpMax;

        public float mpMin => _mpMin;

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
        public event Action<float> onHpMaxIncreased;
        public event Action<float> onMaxHpDecreased;

        public event Action onHpMin;
        public event Action onHpMax;
        public event Action<float> onMpChanged;

        public event Action<float> onMpIncreased;
        public event Action<float> onMpDecreased;
        public event Action<float> onMpMaxIncreased;
        public event Action<float> onMpMaxDecreased;
        public event Action onMpMin;
        public event Action onMpMax;

        public void Damage(GameObject damager, float amount)
        {
            hp -= amount*_takenDamage;
        }

        public void Heal(GameObject healer, float amount)
        {
            hp += amount;
        }

        public void useMp(float amount)
        {
           mp -= amount;
        }

        public void RestoreMp(float amount)
        {
            mp += amount;
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

      
    }
}