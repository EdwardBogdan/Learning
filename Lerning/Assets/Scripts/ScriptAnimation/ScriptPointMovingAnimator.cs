using UnityEngine;
using UnityEngine.Events;

namespace ScriptAnimation
{
    public class ScriptPointMovingAnimator : MonoBehaviour
    {
        [SerializeField] bool _gizmos = true;
        [SerializeField] bool _playing = true;
        [SerializeField] bool _loop;
        [SerializeField] float _speed;
        [Space(10)]
        [SerializeField] Vector3[] _points;
        [Space(10)]
        [SerializeField] UnityEvent _onComplete;

        private int currentTargetIndex;

        private int MaxPoints => _points.Length;
        private void Start()
        {
            transform.position = _points[0];
            currentTargetIndex = 1;
        }
        private void FixedUpdate()
        {
            if (!_playing) return;

            if (transform.position == _points[currentTargetIndex])
            {
                if (currentTargetIndex < MaxPoints - 1)
                {
                    currentTargetIndex++;
                }
                else if (_loop)
                {
                    transform.position = _points[0];
                    currentTargetIndex = 1;
                    _onComplete?.Invoke();
                }
                else
                {
                    _playing = false;
                    _onComplete?.Invoke();
                    return;
                }
            }
            float step = _speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, _points[currentTargetIndex], step);
        }
        private void OnDrawGizmos()
        {
            if (_gizmos)
            {
                Vector3 currentPos;
                Vector3 nextPos;

                for (int x = 0; x < _points.Length - 1; x++)
                {
                    currentPos = _points[x];
                    nextPos = _points[x + 1];

                    Gizmos.color = Color.cyan;
                    Gizmos.DrawLine(currentPos, nextPos);
                }
            }
        }
    }
}
