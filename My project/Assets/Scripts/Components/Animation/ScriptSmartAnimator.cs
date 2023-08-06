using System;
using UnityEngine;
using UnityEngine.Events;

public class ScriptSmartAnimator : MonoBehaviour
{
    [SerializeField] bool _isPlaying = true;
    [SerializeField][Range(1, 60)] int _frameRate = 10;
    [SerializeField] UnityEvent<string> _onComplete;
    [SerializeField] Clip[] _clips;

    SpriteRenderer _renderer;

    float _secondsPerFrame;
    float _nextFrameTime;
    int _currentFrame;
    

    int _currentClip = 0;

    private void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _secondsPerFrame = 1f / _frameRate;
        _nextFrameTime = Time.time;
    }
    public void Play(bool value)
    {
        _isPlaying = value;
        _currentFrame = 0;
    }
    public void SetClip(string name)
    {
        for (int x = 0; x < _clips.Length; x++)
        {
            if (_clips[x].Name == name)
            {
                _currentClip = x;
                _currentFrame = 0;
                break;
            }
        }
    }
    void FixedUpdate()
    {
        if (!_isPlaying) return;
        if (_nextFrameTime > Time.time) return;

        var clip = _clips[_currentClip];
        if (_currentFrame >= clip.Sprites.Length)
        {
            if (clip.Loop)
            {
                _currentFrame = 0;
            }
            else
            {
                enabled = _isPlaying = clip.AllowNextClip;
                clip.ONComplete?.Invoke();
                _onComplete?.Invoke(clip.Name);
                if (clip.AllowNextClip)
                {
                    _currentFrame = 0;
                    _currentClip = (int)Mathf.Repeat(_currentClip + 1, _clips.Length);
                }
            }

            return;
        }

        _renderer.sprite = clip.Sprites[_currentFrame];
        _nextFrameTime += _secondsPerFrame;
        _currentFrame++;
    }

    [Serializable]
    class Clip
    {
        [SerializeField] private string _name;
        [SerializeField] private bool _loop;
        [SerializeField] private Sprite[] sprites;
        [SerializeField] private bool _allowNextClip;
        [SerializeField] private UnityEvent _onComplete;

        public string Name => _name;
        public bool Loop => _loop;
        public Sprite[] Sprites => sprites;
        public bool AllowNextClip => _allowNextClip;
        public UnityEvent ONComplete => _onComplete;
    }
}
