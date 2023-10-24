using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Skul.FSM
{
    //객체에게 적용할 FSM
    public class StateMachine:MonoBehaviour
    {
        public StateType currentType; // 현재 상태를 저장해두는 변수
        public IStateEnumerator<StateType> current; // 현재 상태의 현재 기능
        //객체가 가지는 상태를 상태 타입을 기준으로 저장하는 Dictionary자료구조
        public Dictionary<StateType, IStateEnumerator<StateType>> states;

        public int times;
        public int StopTime;
        /// <summary>
        /// 상태를 변환할때 호출하는 함수
        /// </summary>
        /// <param name="newType"><변환할상태>
        /// <returns bool></returns>
        public bool ChangeState(StateType newType)
        {
            //타입이 같으면 변환하지 않음
            if (currentType == newType)
                return false;
            //현재 상태에서 변환할수 없는 타입으로 판정되면 변환하지 않음
            if (states[newType].canExecute == false)
                return false;

            //상태의 기능들을 초기화함
            states[currentType].Reset();
            //현재상태를 새로운 상태로 변환함
            current = states[newType];
            //현재 상태의 타입을 새로운 상태의 타입으로 변환
            currentType = newType;
            //현재 상태의 기능은 초기화를 하면 None이기 때문에 Start로 진입시켜줌
            current.MoveNext();
            return true;
        }

        private void Update()
        {
            Debug.Log("machine Update");
            ChangeState(current.MoveNext());
        }

        public void InitStates(Dictionary<StateType, IStateEnumerator<StateType>> states)
        {
            Debug.Log("machine initstate");
            this.states= states;
            current = states[currentType];
        }
    }
}
