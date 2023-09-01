using MyProject.Characters;
using System.Collections;
using UnityEngine;

namespace MyProject.Components
{
    public class PointPatrol : Patrol
    {
        [SerializeField] Transform[] _points;
        [SerializeField] float _treshold = 1f;

        private CharacterPhysicController _controller;
        private int _destinationPointIndex;

        private void Awake()
        {
            _controller = GetComponent<CharacterPhysicController>();
        }
        public override IEnumerator DoPatrol()
        {
            while (enabled)
            {
                if (IsOnPoint())
                {
                    _destinationPointIndex = (int)Mathf.Repeat(_destinationPointIndex + 1, _points.Length);
                }

                var direction = _points[_destinationPointIndex].position - transform.position;

                direction.y = 0;
                direction.Normalize();
                _controller.SetDirection(direction);

                yield return null;
            }
        }
        private bool IsOnPoint()
        {
            return (_points[_destinationPointIndex].position - transform.position).magnitude < _treshold;
        }
    }
}

