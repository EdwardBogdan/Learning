using ColliderTouchSystem;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using CustomUnityUtils.UnityEvents;

namespace AnimatorController
{
    public class CreatureAnimatorController : MonoBehaviour
    {
        [SerializeField] private Animator animator;

        [SerializeField] private bool _setGround;
        [SerializeField] private bool _setWall;
        [SerializeField] private bool _setRunning;
        [SerializeField] private bool _setVerticalVelocity;

        [SerializeField] protected TouchComponent _groundTouch;
        [SerializeField] protected TouchComponent _wallTouch;
        [SerializeField] protected Rigidbody2D _rigidbody;

        [SerializeField] private UnityEventRef[] _eventRefs;

        [SerializeField] private UnityEventStrRef[] _eventStringRefs;

        private Action OnSetAnimatorData;

        private Dictionary<string, UnityEvent> events;
        private Dictionary<string, UnityEvent_String> stringEvents;

        #region Propertys
        private bool IsGrounded => _groundTouch.IsTouched;
        private bool IsWalled => _wallTouch.IsTouched;
        private bool IsRunning => _rigidbody.velocity.x != 0;
        private float VerticalVelocity => _rigidbody.velocity.y;
        #endregion
        #region Animator Keys
        protected static readonly int animatorKey_IsGrounded = Animator.StringToHash("IsGrounded");
        protected static readonly int animatorKey_IsRunning = Animator.StringToHash("IsRunning");
        protected static readonly int animatorKey_IsWalled = Animator.StringToHash("IsWalled");
        protected static readonly int animatorKey_VerticalVelocity = Animator.StringToHash("Vertical Velocity");
        #endregion

        private void FixedUpdate()
        {
            OnSetAnimatorData.Invoke();
        }
        private void Awake()
        {
            events = new Dictionary<string, UnityEvent>(_eventRefs.Length);

            foreach (var item in _eventRefs)
            {
                events[item._key] = item._event;
            }

            _eventRefs = null;

            stringEvents = new Dictionary<string, UnityEvent_String>(_eventStringRefs.Length);

            foreach (var item in _eventStringRefs)
            {
                stringEvents[item._key] = item._event;
            }

            _eventStringRefs = null;
        }
        private void Start()
        {
            OnSetAnimatorData = null;

            if (_setGround) OnSetAnimatorData += () => animator.SetBool(animatorKey_IsGrounded, IsGrounded);
            if (_setRunning) OnSetAnimatorData += () => animator.SetBool(animatorKey_IsRunning, IsRunning);
            if (_setWall) OnSetAnimatorData += () => animator.SetBool(animatorKey_IsWalled, IsWalled);
            if (_setVerticalVelocity) OnSetAnimatorData += () => animator.SetFloat(animatorKey_VerticalVelocity, VerticalVelocity);

            enabled = OnSetAnimatorData != null;
        }

        public void InvokeEvent(string key)
        {
            UnityEvent _event = GetEvent(key);

            _event.Invoke();
        }
        public UnityEvent GetEvent(string key)
        {
            return events[key];
        }
        public void InvokeStringEvent(string key)
        {
            string[] keys = key.Split(", ");

            UnityEvent_String _event = GetStringEvent(keys[0]);

            _event.Invoke(keys[1]);
        }
        public UnityEvent_String GetStringEvent(string key)
        {
            return stringEvents[key];
        }


        [Serializable]
        private class UnityEventRef
        {
            public string _key;
            public UnityEvent _event;
        }

        [Serializable]
        private class UnityEventStrRef
        {
            public string _key;
            public UnityEvent_String _event;
        }
    }
}
