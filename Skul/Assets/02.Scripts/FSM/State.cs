using System.Linq;
using UnityEngine;
using Skul.Character;
using System.Collections.Generic;
using System.Collections;

namespace Skul.FSM
{
    public enum StateType
    {
        Idle,
        Move,
        Jump,
        DownJump,
        Fall,
        Attack,
        Skill_1,
        Skill_2,
        Die
    }


    public abstract class State : IStateEnumerator<StateType>
    {
        public abstract bool canExecute { get; }
        public IStateEnumerator<StateType>.Step current => currentStep;

       // public StateType Current => throw new System.NotImplementedException();

        //object IEnumerator.Current => throw new System.NotImplementedException();

        protected IStateEnumerator<StateType>.Step currentStep;
        protected StateMachine machine;
        protected Animator animator;
        protected Rigidbody2D rigid;
        protected CapsuleCollider2D trigger;
        protected CapsuleCollider2D collider;
        protected Transform transform;
        protected Skul.Character.Character character;

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
            this.character = machine.GetComponent<Skul.Character.Character>();

        }

        public abstract StateType MoveNext();

        public virtual void Reset()
        {
            currentStep = IStateEnumerator<StateType>.Step.None;
        }

        bool IEnumerator.MoveNext()
        {
            throw new System.NotImplementedException();
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }
    }
}