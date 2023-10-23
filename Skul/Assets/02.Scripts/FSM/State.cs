using System.Linq;
using UnityEngine;
using Skul.Character;
using System.Collections.Generic;
using System.Collections;

namespace Skul.FSM
{
    //상태 목록
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

    //각 상태들이 상속받을 부모 클래스
    public abstract class State : IStateEnumerator<StateType>
    {
        //현재 상태와 변환될 상태를 비교하여 변환될 상태가 실행가능한지를 저장하는 변수
        public abstract bool canExecute { get; }
        //현재상태를 외부에 사용할 수 있게 해주는 변수
        public IStateEnumerator<StateType>.Step current => currentStep;
      
        //현재 상태의 진행을 저장
        protected IStateEnumerator<StateType>.Step currentStep;
        //statemachine을 가지는 객체의 컴포넌트들을 저장하는 변수들
        protected StateMachine machine;
        protected Animator animator;
        protected Rigidbody2D rigid;
        protected CapsuleCollider2D trigger;
        protected CapsuleCollider2D collider;
        protected Transform transform;
        protected Skul.Movement.Movement movement; 
        protected Skul.Character.Character character;

        //생성자
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