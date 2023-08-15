using MyProject.Components;
using UnityEngine;

namespace MyProject.Props
{
    public class Barrel : MonoBehaviour
    {
        [SerializeField] GroundCheckComponent _ground;
        [SerializeField] AutoBoxAllCastComponent _fallHitCast;
        ScriptSmartAnimator _animator;
        
        static readonly string keyVibration = "Vibration";
        static readonly string keyHit = "Hit";


        private void Awake()
        {
            _animator = GetComponent<ScriptSmartAnimator>();
        }
        private void Update()
        {
            if (!_ground.isGrounded)
            {
                _fallHitCast.enabled = true;
            }
        }
        public void Hit()
        {
            _animator.SetClip(keyHit);
        }
        public void Vibration()
        {
            _animator.SetClip(keyVibration);
        }
    }
}
