using UnityEngine;
using UnityEngine.Events;
using System.Collections;

namespace MyProject.Components
{
    public class TransparencyProcessor : MonoBehaviour
    {
        
        [SerializeField][Range(0,1)] private float _startValue;
        [SerializeField][Range(0, 1)] private float _finishValue;
        [SerializeField] private float duration;
        [SerializeField] private SpriteRenderer[] _renderers;
        [Space(10)]
        [SerializeField] private UnityEvent _onFinish;

        public void StartProcess()
        {
            StartCoroutine(Process());
        }
        private IEnumerator Process()
        {
            float currentTime = 0f;

            while (currentTime < duration)
            {
                currentTime += Time.deltaTime;

                float t = currentTime / duration;

                float currentValue = Mathf.Lerp(_startValue, _finishValue, t);

                foreach (var renderer in _renderers)
                {
                    Color color = renderer.color;
                    color.a = currentValue;
                    renderer.color = color;
                }

                yield return null;
            }

            _onFinish?.Invoke();
        }
    }
}
