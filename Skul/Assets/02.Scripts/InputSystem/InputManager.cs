using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Skul.Tools;


namespace Skul.InputSystem
{
    //
    public class InputManager : SingletonMonoBase<InputManager>
    {
        //키 입력(유형)-기능(Action)을 저장한 dictionary들을 가지는 객체  
        public class Map
        {
            //-1~1 사이의 입력(axis)을 가지는 Dictionary
            private Dictionary<string, Action<float>> _axisActions = new Dictionary<string, Action<float>>();
            //-1,0,1 의 입력(rawaxis) 가지는 Dictionary
            private Dictionary<string, Action<float>> _rawAxisActions = new Dictionary<string, Action<float>>();
            //키다운시 액션을 저장해두는 Dictionary
            private Dictionary<KeyCode, Action> _keyDownActions = new Dictionary<KeyCode, Action>();
            //키를 누르는 중에 발생하는 액션을 저장해두는 Dictionary
            private Dictionary<KeyCode, Action> _keyPressActions = new Dictionary<KeyCode, Action>();
            //키를 떌때 발생하는 액션을 저장해 두는 Dictionary
            private Dictionary<KeyCode, Action> _keyUpActions = new Dictionary<KeyCode, Action>();

            public void AddAxisAction(string axis, Action<float> action)
            {
                if (_axisActions.ContainsKey(axis))
                    _axisActions[axis] += action;
                else
                    _axisActions.Add(axis, action);
            }

            public void AddRawAxisAction(string axis, Action<float> action)
            {
                if (_rawAxisActions.ContainsKey(axis))
                    _rawAxisActions[axis] += action;
                else
                    _rawAxisActions.Add(axis, action);
            }

            public void AddKeyDownAction(KeyCode key, Action action)
            {
                if (_keyDownActions.ContainsKey(key))
                    _keyDownActions[key] += action;
                else
                    _keyDownActions.Add(key, action);
            }

            public void AddKeyPressAction(KeyCode key, Action action)
            {
                if (_keyPressActions.ContainsKey(key))
                    _keyPressActions[key] += action;
                else
                    _keyPressActions.Add(key, action);
            }

            public void AddKeyUpAction(KeyCode key, Action action)
            {
                if (_keyUpActions.ContainsKey(key))
                    _keyUpActions[key] += action;
                else
                    _keyUpActions.Add(key, action);
            }

            
            public void InvokeAll()
            {
                foreach (var item in _axisActions)
                {
                    item.Value.Invoke(Input.GetAxis(item.Key));
                }

                foreach (var item in _rawAxisActions)
                {
                    item.Value.Invoke(Input.GetAxisRaw(item.Key));
                }

                foreach (var item in _keyDownActions)
                {
                    if (Input.GetKeyDown(item.Key))
                        item.Value.Invoke();
                }

                foreach (var item in _keyPressActions)
                {
                    if (Input.GetKey(item.Key))
                        item.Value.Invoke();
                }

                foreach (var item in _keyUpActions)
                {
                    if (Input.GetKeyUp(item.Key))
                        item.Value.Invoke();
                }
            }

        }
        
        //현재 Map객체를 사용할 수 있는 상태인지 
        public bool enabledCurrent { get; set; }
        //이름-Map객체 로 연결하여 저장하는 Dictionary
        public Dictionary<string, Map> maps = new Dictionary<string, Map>();
        //현재 사용중인 Map객체
        public Map currentmap;

        
        public void AddMap(string mapName, Map map)
        {
            //동일한 이름의 Map객체가 있으면저장하고 현재 사용중인 Map객체가 없으면 현재 사용중인 객체로 지정
            if (maps.TryAdd(mapName, map))
            {
                if (currentmap == null)
                    currentmap = map;
            }
            else
            {
                //Alert some notification that mapName has already been registered
                throw new Exception("Same mapName is already had Dictionary");
            }
        }

        private void Awake()
        {
            enabledCurrent = true;
        }

        private void Update()
        {
            if (enabledCurrent)
                currentmap.InvokeAll();
        }

    }
}