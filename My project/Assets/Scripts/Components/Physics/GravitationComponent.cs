using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace MyProject.Components
{
    public class GravitationComponent : MonoBehaviour
    {
        public Vector2 LastSpeed { get; private set; }

        [SerializeField] float _coefficient;
        [SerializeField] TouchComponent groundChecker;
        [SerializeField] UnityEvent _fallingAction;
        [SerializeField] GroundEvent[] _events;
        

        float _sumCoefficient;

        Rigidbody2D rigidBody;
        

        private void Start()
        {
            rigidBody = GetComponent<Rigidbody2D>();
            DropSpeed();
        }
        private void FixedUpdate()
        {
            if (groundChecker.IsTouched)
            {
                if (LastSpeed.y <= -2)
                {
                    Grounding();
                }
            }
            else if (rigidBody.velocity.y < -2)
            {
                _fallingAction?.Invoke();
                LastSpeed = rigidBody.velocity;
                _sumCoefficient += _coefficient;
                rigidBody.velocity = new Vector2(LastSpeed.x, LastSpeed.y - 1 * _sumCoefficient);
            }
        }
        public void DropSpeed()
        {
            _sumCoefficient = 0;
            LastSpeed = Vector2.zero;
        }

        private void Grounding()
        {
            foreach (GroundEvent _event in _events)
            {
                if (LastSpeed.y <= _event._speedMark)
                {
                    _event?._action.Invoke();
                    break;
                }
            }
            DropSpeed();
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
