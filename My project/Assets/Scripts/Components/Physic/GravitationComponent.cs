using System;
using UnityEngine;
using UnityEngine.Events;
using System.Collections;

namespace MyProject.Components.Physic
{
    public class GravitationComponent : MonoBehaviour
    {
        [SerializeField] Rigidbody2D _rb;
        [SerializeField] GroundEvent[] _events;

        Coroutine _currentCoroutine;

        float _currentDistance;
        public void OnJumping()
        {
            if (_currentCoroutine != null) StopCoroutine(_currentCoroutine);

            _currentCoroutine = StartCoroutine(Counting());

        }
        public void OnGrounding()
        {
            foreach (GroundEvent _event in _events)
            {
                if (_currentDistance <= _event._speedMark)
                {
                    _event?._action.Invoke();
                    break;
                }
            }

            if (_currentCoroutine != null) StopCoroutine(_currentCoroutine);

            Debug.Log($"{_rb.gameObject.name} falls from: {_currentDistance}");

            _currentDistance = 0;
        }
        private IEnumerator Counting()
        {
            while (true)
            {
                float y = _rb.velocity.y;

                _currentDistance = (y * y) / (2 * Physics2D.gravity.y);

                yield return null;
            }
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
