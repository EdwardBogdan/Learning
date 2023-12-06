using System;
using UnityEngine;

namespace CustomUnityUtils.TimeUtils
{
    [Serializable]
    public class Cooldown
    {
        [SerializeField] private float _time;

        private float _timesUp;

        public Cooldown(float time)
        {
            _time = time;
            _timesUp = 0f;
        }
        public bool IsReady => _timesUp <= Time.time;
        public void Reset()
        {
            _timesUp = Time.time + _time;
        }
        public void Ready()
        {
            _timesUp = Time.time;
        }
    }
}
