using System.Linq;
using UnityEngine;
using Skul.Character;
using System.Collections.Generic;
using System.Collections;

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
        Die
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
        protected Animator animator;
        protected Rigidbody2D rigid;
        protected CapsuleCollider2D trigger;
        protected CapsuleCollider2D collider;
        protected Transform transform;
        protected Skul.Movement.Movement movement; 
        protected Skul.Character.Character character;

        //������
        public State(StateMachine machine)
        {
            this.machine = machine;
            this.animator=machine.GetComponentInChildren<Animator>();
            this.rigid=machine.GetComponentInChildren<Rigidbody2D>();
            this.trigger=machine.GetComponentsInChildren<CapsuleCollider2D>().
                Where(c=>c.isTrigger==true).First();
            this.collider = machine.GetComponentsInChildren<CapsuleCollider2D>().
                Where(c => c.isTrigger == false).First();
            this.transform=machine.GetComponent<Transform>();
            this.movement = machine.GetComponent<Skul.Movement.Movement>();
            this.character = machine.GetComponent<Skul.Character.Character>();
        }


        public abstract StateType MoveNext();

        public virtual void Reset()
        {
            currentStep = IStateEnumerator<StateType>.Step.None;
        }
    }
}