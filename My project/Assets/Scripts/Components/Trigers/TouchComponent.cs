using MyProject.Utils;
using UnityEngine;

namespace MyProject.Components.Triggers
{
    public class TouchComponent : MonoBehaviour
    {
        [SerializeField] bool _isTouched;
        [SerializeField] float _lastTouchTime;
        public float LastTouchTime => _lastTouchTime;
        public bool IsTouched => _isTouched;
        public void Touch(GameObject hit)
        {
            if (hit == null && _isTouched)
            {
                _lastTouchTime = Time.time;
            }
            _isTouched = hit != null;
        }
    }
}
