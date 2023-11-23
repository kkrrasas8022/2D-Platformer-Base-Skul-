using System.Linq;
using UnityEngine;
using Skul.Character;
using System.Collections.Generic;
using System.Collections;
using Unity.VisualScripting;
using System;
using Skul.Character.PC;
//using System.Diagnostics;

namespace Skul.FSM
{
    //���� ���
    public enum StateType
    {
        Idle,
        Move,
        Jump,
        Dash,
        DownJump,
        Fall,
        Attack,
        Skill_1,
        Skill_2,
        Die,
        Hurt,
        JumpAttack,
        Switch,
        Charging
    }

    //�� ���µ��� ��ӹ��� �θ� Ŭ����
    public abstract class State : IStateEnumerator<StateType>
    {
        //���� ���¿� ��ȯ�� ���¸� ���Ͽ� ��ȯ�� ���°� ���డ�������� �����ϴ� ����
        public abstract bool canExecute { get; }
        //������¸� �ܺο� ����� �� �ְ� ���ִ� ����
        public IStateEnumerator<StateType>.Step current => currentStep;
      
        //���� ������ ������ ����
        protected IStateEnumerator<StateType>.Step currentStep;
        //statemachine�� ������ ��ü�� ������Ʈ���� �����ϴ� ������
        protected StateMachine machine;
        //protected Animator animator;
        public Animator animator;
        protected Rigidbody2D rigid;
        protected BoxCollider2D trigger;
        protected BoxCollider2D collider;
        protected Transform transform;
        protected Skul.Movement.Movement movement;
        protected Skul.Character.Character character;

        //������
        public State(StateMachine machine)
        {
            this.machine = machine;
            this.animator = machine.GetComponentInChildren<Animator>();
            this.rigid=machine.GetComponent<Rigidbody2D>();
            this.trigger=machine.GetComponentsInChildren<BoxCollider2D>().
                Where(c=>c.isTrigger==true).First();
            this.collider = machine.GetComponentsInChildren<BoxCollider2D>().
                Where(c => c.isTrigger == false).First();
            this.transform=machine.GetComponent<Transform>();
            this.movement = machine.GetComponent<Skul.Movement.Movement>();
            this.character = machine.GetComponent<Skul.Character.Character>();
            machine.OnAnimatorChanged += (value) =>
            {
                Debug.Log("����");
                animator = value;
            };
            this.machine.GetComponent<Character.Character>().onHpMin += () => this.machine.isDie = true;
        }

        

        public abstract StateType MoveNext();

        public virtual void Reset()
        {
            currentStep = IStateEnumerator<StateType>.Step.None;
        }
    }
}