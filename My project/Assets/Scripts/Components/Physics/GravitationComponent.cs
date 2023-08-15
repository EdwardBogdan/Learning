using System;
using UnityEngine;
using UnityEngine.Events;

namespace MyProject.Components
{
    public class GravitationComponent : MonoBehaviour
    {
        public Vector2 _lastSpeed { get; private set; }

        [SerializeField] float _coefficient;
        [SerializeField] GroundEvent[] _events;
        [SerializeField] GroundCheckComponent groundChecker;

        float _sumCoefficient;

        Rigidbody2D rigidBody;
        

        private void Start()
        {
            rigidBody = GetComponent<Rigidbody2D>();
            Reseting();
        }
        private void FixedUpdate()
        {
            if (groundChecker.isGrounded)
            {
                if (_lastSpeed.y <= -2)
                {
                    Grounding();
                }
            }
            else if (rigidBody.velocity.y < -2)
            {
                _lastSpeed = rigidBody.velocity;
                _sumCoefficient += _coefficient;
                rigidBody.velocity = new Vector2(_lastSpeed.x, _lastSpeed.y - 1 * _sumCoefficient);
            }
        }
        public void Reseting()
        {
            _sumCoefficient = 0;
            _lastSpeed = Vector2.zero;
        }

        void Grounding()
        {
            foreach (GroundEvent _event in _events)
            {
                if (_lastSpeed.y <= _event._speedMark)
                {
                    _event?._action.Invoke();
                }
            }
            Reseting();
        }
    }
    [Serializable]
    public class GroundEvent
    {
        [Tooltip("Hint: Fall rate is negative")]
        public float _speedMark;
        public UnityEvent _action;
    }
}
