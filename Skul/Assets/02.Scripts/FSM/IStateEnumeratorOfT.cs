

namespace Skul.FSM
{
    //FSM은 유닛의 상태를 기반으로 동작을 제어하는 방식을 구현하기 위한 디자인패턴
    //상태를 진행하는 것을 IEnumerator을 기반으로 하는 인터페이스로 구현하도록 한다 
    public interface IStateEnumerator<T>
        where T:System.Enum
    {
        //상태의 진행을 나타내는 열거형 타입
        public enum Step
        {
            None,
            Start,
            Casting,
            DoAction,
            WaitUntilActionFinished,
            Finish
        }

        //현재 상태
        Step current { get; }
        //상태를 바꿀 수 있는지 판별하는 변수
        bool canExecute { get; }
        //상태의 step을 다음으로 변경하고 상태를 리턴하는 함수 
        T MoveNext();
        //열거형을 초기화 하는 함수
        void Reset();
    }
}
