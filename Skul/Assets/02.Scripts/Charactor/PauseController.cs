using Skul.InputSystem;
using Skul.Tools;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Skul.Character
{
    public class PauseController:SingletonBase<PauseController>
    {
        public enum State
        {
            None,
            playing,
            Pause
        }
        public State state;

        private List<IPausable> _pausables = new List<IPausable>();

        public void Register(IPausable pausable)
        {
            _pausables.Add(pausable);
            pausable.Pause(state == State.Pause);
        }

        public Action OnPause;

        protected override void Init()
        {
            base.Init();
            OnPause += () =>
            { 
                state = state == State.Pause ? State.playing : State.Pause;
                //InputManager.instance.enabledCurrent = state == State.Pause ? false : true;
                Time.timeScale = state == State.Pause ? 0.0f : 1.0f;
                foreach (var pausable in _pausables)
                {
                    pausable.Pause(state == State.Pause);
                }
            };
        }
    }
}