using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace MyProject.Components
{
    public class CheatController : MonoBehaviour
    {
        [SerializeField] float _inputLiveTime;
        [SerializeField] CheatItem[] _cheats;

        string _currentInput;
        float _inputTime;

        private void Awake()
        {
            Keyboard.current.onTextInput += OnTextInput;
        }
        void OnTextInput(char inputChar)
        {
            _currentInput += inputChar;
            _inputTime = _inputLiveTime;
            FindAnyCheats();
        }
        void FindAnyCheats()
        {
            foreach (CheatItem cheat in _cheats)
            {
                if (_currentInput.Contains(cheat._name))
                {
                    cheat._action?.Invoke();
                    _currentInput = string.Empty;
                }
            }
        }
        void Update()
        {
            if (_inputTime < 0)
            {
                _currentInput = string.Empty;
            }
            else
            {
                _inputTime -= Time.deltaTime;
            }
        }
    }
    [Serializable]
    public class CheatItem
    {
        public string _name;
        public UnityEvent _action;
    }
}
