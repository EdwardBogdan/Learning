using UnityEngine;
using UnityEngine.Events;

namespace MyProject.Components.ScriptAnimations
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class ScriptAnimator : MonoBehaviour
    {
        [SerializeField] bool _playing = true;
        [SerializeField] int _frameRate;
        [SerializeField] bool _loop;
        [SerializeField] Sprite[] _sprites;
        [SerializeField] UnityEvent _onComplete;

        SpriteRenderer _renderer;
        float _secontPerFrame;
        int _currentSprite;
        float _nextFrameTime;

        private void Start()
        {
            _renderer = GetComponent<SpriteRenderer>();
            _secontPerFrame = 1f / _frameRate;
            _nextFrameTime = Time.time + _secontPerFrame;
        }

        void Update()
        {
            if (!_playing || _nextFrameTime > Time.time) return;

            if (_currentSprite >= _sprites.Length)
            {
                if (_loop)
                {
                    _currentSprite = 0;
                }
                else
                {
                    _playing = false;
                    _onComplete?.Invoke();
                    return;
                }
            }

            _renderer.sprite = _sprites[_currentSprite];
            _nextFrameTime += _secontPerFrame;
            _currentSprite++;
        }
    }
}
