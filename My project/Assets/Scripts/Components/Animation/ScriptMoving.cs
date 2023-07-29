using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MyProject.Components
{
    public class ScriptMoving : MonoBehaviour
    {
        [SerializeField] bool _gizmos = true;
        [SerializeField] bool _playing = true;
        [SerializeField] bool _loop;
        [SerializeField] float _speed;
        [Space(10)]
        [SerializeField] Vector3 _finishPosition;
        Vector3 _startPosition;
        [Space(10)]
        [SerializeField] UnityEvent _onComplete;


        private void Start()
        {
            _startPosition = transform.position;
        }
        private void Update()
        {
            if (!_playing) return;

            if (transform.position == _finishPosition)
            {
                if (_loop)
                {
                    transform.position = _startPosition;
                }
                else
                {
                    _playing = false;
                    _onComplete?.Invoke();
                    return;
                }
            }
            float step = _speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, _finishPosition, step);
        }
        private void OnDrawGizmos()
        {
            if (_gizmos)
            {
                Gizmos.color = Color.cyan;
                Gizmos.DrawLine(transform.position, _finishPosition);
            }
        }
    }
}
