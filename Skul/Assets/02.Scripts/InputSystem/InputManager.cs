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
        //Ű �Է�(����)-���(Action)�� ������ dictionary���� ������ ��ü  
        public class Map
        {
            //-1~1 ������ �Է�(axis)�� ������ Dictionary
            private Dictionary<string, Action<float>> _axisActions = new Dictionary<string, Action<float>>();
            //-1,0,1 �� �Է�(rawaxis) ������ Dictionary
            private Dictionary<string, Action<float>> _rawAxisActions = new Dictionary<string, Action<float>>();
            //Ű�ٿ�� �׼��� �����صδ� Dictionary
            private Dictionary<KeyCode, Action> _keyDownActions = new Dictionary<KeyCode, Action>();
            //Ű�� ������ �߿� �߻��ϴ� �׼��� �����صδ� Dictionary
            private Dictionary<KeyCode, Action> _keyPressActions = new Dictionary<KeyCode, Action>();
            //Ű�� ���� �߻��ϴ� �׼��� ������ �δ� Dictionary
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
        
        //���� Map��ü�� ����� �� �ִ� �������� 
        public bool enabledCurrent { get; set; }
        //�̸�-Map��ü �� �����Ͽ� �����ϴ� Dictionary
        public Dictionary<string, Map> maps = new Dictionary<string, Map>();
        //���� ������� Map��ü
        public Map currentmap;

        
        public void AddMap(string mapName, Map map)
        {
            //������ �̸��� Map��ü�� �����������ϰ� ���� ������� Map��ü�� ������ ���� ������� ��ü�� ����
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