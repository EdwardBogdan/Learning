using System;
using UnityEngine;
using UnityEngine.Events;

namespace ScriptAnimation
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class ScriptSmartAnimator : MonoBehaviour
    {
        [SerializeField][Range(1, 60)] private int _frameRate = 10;
        [SerializeField] private Clip[] _clips;

        
        private float _secondsPerFrame;
        private float _nextFrameTime;
        private int _currentFrame;
        private int _clipLenght;

        private SpriteRenderer _renderer;
        private Clip _currentClip;


        public void SetClip(string name)
        {
            foreach (var clip in _clips)
            {
                if (clip.Name == name)
                {
                    _currentClip = clip;
                    _clipLenght = clip.Sprites.Length;
                    _currentFrame = 0;

                    _nextFrameTime = Time.time + _secondsPerFrame;
                    enabled = true;
                    break;
                }
            }
        }
        private void Awake()
        {
            _renderer = GetComponent<SpriteRenderer>();
        }
        private void Start()
        {
            _secondsPerFrame = 1f / _frameRate;
            _nextFrameTime = Time.time + _secondsPerFrame;

            if (_clips.Length <= 0)
            {
                enabled = false;
                return;
            }

            SetClip(_clips[0].Name);
        }
        private void FixedUpdate()
        {
            if (_nextFrameTime > Time.fixedTime) return;

            if (_currentFrame >= _clipLenght)
            {
                if (!_currentClip.Loop)
                {
                    enabled = false;
                    _currentClip.ONComplete?.Invoke();
                }

                _currentFrame = 0;
                return;
            }

            _nextFrameTime += _secondsPerFrame;
            _renderer.sprite = _currentClip.Sprites[_currentFrame];
            _currentFrame++;
        }

        [Serializable]
        private class Clip
        {
            [SerializeField] private string _name;
            [SerializeField] private bool _loop;
            [SerializeField] private Sprite[] sprites;
            [SerializeField] private UnityEvent _onComplete;

            internal string Name => _name;
            internal bool Loop => _loop;
            internal Sprite[] Sprites => sprites;
            internal UnityEvent ONComplete => _onComplete;
        }
    }
}