using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyProject.Objects
{
    public class Barrel : MonoBehaviour
    {
        Rigidbody2D _body;
        Animator _animator;
        bool _falling;
        static readonly int keyVibration = Animator.StringToHash("Vibration");

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _body = GetComponent<Rigidbody2D>();
        }
        private void FixedUpdate()
        {
            if (_body.velocity.y < -2)
            {
                _falling = true;
            }
            if (_falling && _body.velocity.y >= 0)
            {
                Vibration();
                _falling = false;
            }
        }
        public void Vibration()
        {
            _animator.SetTrigger(keyVibration);
        }
    }
}
