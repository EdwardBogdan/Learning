using UnityEngine;
using UnityEngine.Events;
using System.Collections;

namespace MyProject.Components
{
    public class TransparencyProcessor : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _renderer;
        [SerializeField] private float endValue;
        [SerializeField] private float duration;
        [SerializeField] private UnityEvent _onFinish;

        private float currentTime = 0f;

        public void StartProcess()
        {
            StartCoroutine(Process());
        }
        public void SetTime(float value)
        {
            duration = value;
        }
        public void SetEndValue(float value)
        {
            endValue = value;
        }
        private IEnumerator Process()
        {
            Color currentColor = _renderer.color;

            float startValue = currentColor.a;

            while (currentTime < duration)
            {
                currentTime += Time.deltaTime;

                float t = currentTime / duration;

                currentColor.a = Mathf.Lerp(startValue, endValue, t);

                _renderer.color = currentColor;

                yield return null;
            }

            _onFinish?.Invoke();
        }
    }
}
