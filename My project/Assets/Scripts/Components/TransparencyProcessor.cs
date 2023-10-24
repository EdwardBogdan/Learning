using UnityEngine;
using UnityEngine.Events;
using System.Collections;

namespace MyProject.Components
{
    public class TransparencyProcessor : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _renderer;
        [SerializeField] private float duration;
        [SerializeField] private UnityEvent _onFinish;

        private float currentTime = 0f;

        public void StartProcess(float value)
        {
            StartCoroutine(Process(value));
        }
        public void SetTime(float value)
        {
            duration = value;
        }
        private IEnumerator Process(float value)
        {
            Color currentColor = _renderer.color;

            float startValue = currentColor.a;

            while (currentTime < duration)
            {
                currentTime += Time.deltaTime;

                float t = currentTime / duration;

                currentColor.a = Mathf.Lerp(startValue, value, t);

                _renderer.color = currentColor;

                yield return null;
            }

            _onFinish?.Invoke();
        }
    }
}
