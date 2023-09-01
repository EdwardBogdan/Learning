using System;
using System.Collections;
using UnityEngine;

namespace MyProject.Components
{
    public class DropProcessor : MonoBehaviour
    {
        [SerializeField] AnimationCurve _curve;

        [SerializeField] float duration = 3.0f;

        [Header("Drop Range")]
        [SerializeField] Vector2 Max;
        [SerializeField] Vector2 Min;

        public void Droping(Drop[] _dropList)
        {
            foreach (Drop item in _dropList)
            {
                int count = 0;

                if (item._chanceToEach)
                {
                    for (int x = 0; x < item._count; x++)
                    {
                        int randomValue = UnityEngine.Random.Range(0, 101);

                        if (randomValue <= item._chance)
                        {
                            count++;
                        }
                    }
                }
                else
                {
                    int randomValue = UnityEngine.Random.Range(0, 101);

                    if (randomValue <= item._chance)
                    {
                        count = item._count;
                    }
                }

                for (int x = 0; x < count; x++)
                {
                    GameObject droppedObject = Instantiate(item._drop, transform.position, Quaternion.identity);
                    StartCoroutine(MoveAlongCurve(droppedObject));
                }
                StartCoroutine(LifeTimer());
            }
        }

        private IEnumerator MoveAlongCurve(GameObject _object)
        {
            Collider2D collider = _object.GetComponent<Collider2D>();
            if (collider != null)
            {
                collider.enabled = false;
            }
            float startTime = Time.time;
            float endTime = startTime + duration;
            float timeScale = _curve.keys[_curve.length - 1].time;

            Vector2 startPos = _object.transform.position;

            float _distance = UnityEngine.Random.Range(Min.x, Max.x);
            int randomValue = UnityEngine.Random.Range(0, 2); // Случайное значение от 0 до 1
            _distance *= ((randomValue == 0) ? -1 : 1); // Преобразуем 0 в -1

            float _height = UnityEngine.Random.Range(Min.y, Max.y);

            while (Time.time < endTime)
            {
                float timeFraction = (Time.time - startTime) / duration;
                float scaledTime = timeFraction * timeScale;
                float curveValue = _curve.Evaluate(scaledTime);

                Vector3 newPosition = new()
                {
                    x = Mathf.Lerp(startPos.x, startPos.x + _distance, timeFraction),
                    y = Mathf.Lerp(startPos.y, startPos.y + _height, curveValue)
                };

                if (_object == null) break;

                _object.transform.position = newPosition;

                yield return null;
            }
            if (collider != null)
            {
                collider.enabled = true;
            }
        }
        private IEnumerator LifeTimer()
        {
            yield return new WaitForSeconds(duration + 1);
            Destroy(gameObject);
        }
    }

    
}
