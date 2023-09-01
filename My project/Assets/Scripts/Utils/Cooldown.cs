using System;
using UnityEngine;

namespace MyProject.Utils
{
    [Serializable]
    public class Cooldown
    {
        [SerializeField] private float _time;

        private float _timesUp;

        public bool IsReady => _timesUp <= Time.time;
        public void Reset()
        {
            _timesUp = Time.time + _time;
        }
    }
}
