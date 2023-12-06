using System;
using UnityEngine;
using UnityEngine.Events;

namespace PhysicComponents
{
    public class GravitationComponent : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rb;
        [Tooltip("Events should be sorted by speed, moving to zero at the end of the mas")]
        [SerializeField] private GroundEvent[] _events;

        float _currentDistance;

        public void OnGrounding()
        {
            foreach (GroundEvent _event in _events)
            {
                if (_currentDistance <= _event._invokeYSpeed)
                {
                    _event?._action.Invoke();
                    break;
                }
            }

            Debug.Log($"{_rb.gameObject.name} reached the limit of: {_currentDistance}");

            enabled = false;

            _currentDistance = 0;
        }

        private void FixedUpdate()
        {
            float y = _rb.velocity.y;

            _currentDistance = (y * y) / (2 * Physics2D.gravity.y);
        }
        [Serializable]
        private class GroundEvent
        {
            [Tooltip("Hint: Fall rate might be negative")]
            public float _invokeYSpeed;
            public UnityEvent _action;
        }
    }
    
}
