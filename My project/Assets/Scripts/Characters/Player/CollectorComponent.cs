using System.Collections;
using UnityEngine;

namespace MyProject.Components
{
    public class CollectorComponent : MonoBehaviour
    {
        [SerializeField] float _duration;

        public void Collect(GameObject _object)
        {
            StartCoroutine(MoveToPosition(_object));
        }
        private IEnumerator MoveToPosition(GameObject _object)
        {
            float startTime = Time.time;
            float endTime = startTime + _duration;

            Vector3 startPos = _object.transform.position;

            while (Time.time < endTime)
            {
                float timeFraction = (Time.time - startTime) / _duration;

                Vector3 newPosition = new()
                {
                    x = Mathf.Lerp(startPos.x, transform.position.x, timeFraction),
                    y = Mathf.Lerp(startPos.y, transform.position.y, timeFraction)
                };

                if (_object == null) break;

                _object.transform.position = newPosition;

                yield return null;
            }

            if (_object != null)
            {
                _object.transform.position = transform.position; // Убедимся, что объект точно будет в нужной позиции
            }
        }
    }
}
