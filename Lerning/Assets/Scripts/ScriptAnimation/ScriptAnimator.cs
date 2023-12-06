using UnityEngine;
using UnityEngine.Events;

namespace ScriptAnimation
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class ScriptAnimator : MonoBehaviour
    {
        [SerializeField][Range(1, 60)] private int _frameRate = 10;
        [SerializeField] private bool _loop;
        [SerializeField] private Sprite[] _sprites;
        [SerializeField] private UnityEvent _onComplete;

        private int _currentSprite;
        private int _spriteLenght;
        private float _secondsPerFrame;
        private float _nextFrameTime;

        private SpriteRenderer _renderer;

        public void Play(bool value)
        {
            enabled = value;
            _currentSprite = 0;
            _nextFrameTime = Time.time + _secondsPerFrame;
        }
        public void SetLoop(bool value)
        {
            _loop = value;
        }

        private void Awake()
        {
            _renderer = GetComponent<SpriteRenderer>();
        }
        private void Start()
        {
            _secondsPerFrame = 1f / _frameRate;
            _nextFrameTime = Time.time;
            _spriteLenght = _sprites.Length;
        }

        private void Update()
        {
            if (_nextFrameTime > Time.time) return;

            if (_currentSprite >= _spriteLenght)
            {
                if (!_loop)
                {
                    enabled = false;
                    _onComplete?.Invoke();
                    return;
                }
                _currentSprite = 0;
            }

            _renderer.sprite = _sprites[_currentSprite];
            _nextFrameTime += _secondsPerFrame;
            _currentSprite++;
        }
    }
}
