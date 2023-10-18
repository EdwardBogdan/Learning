using MyProject.Components.Triggers;
using MyProject.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyProject.Physic
{
    //  WARNING: This component not perfect :)

    public class OLD_PerfectPhysicController : MonoBehaviour
    {
        
        [Header("WALKING")]
        [SerializeField] private float _acceleration;
        [SerializeField] private float _moveClamp;
        [SerializeField] private float _deAcceleration;

        [Header("JUMPING")]
        [SerializeField] private float _jumpHeight;
        [SerializeField][Range(0, 5)] int _countDoubleJumpMax;
        [SerializeField] private float _coyoteTime;
        [SerializeField] private float _fallMultiplier;
        [SerializeField] private float _lowJumpMultiplier;
        [SerializeField] protected Cooldown _jumpCooldown;

        [Header("OTHER VALUES")]
        [SerializeField] protected Cooldown _platformCollision;

        [Header("COMPONENTS")]
        [SerializeField] protected TouchComponent _ground;
        [SerializeField] protected TouchComponent _wall;

        protected Rigidbody2D _rb;
        protected List<Collider2D> _currentPlatforms = new();
        private Vector2 _direction;
        private int _countDoubleJump;

        private bool _coyoteUsable;
        private float TimeLeftGrounded => _ground.LastTouchTime;
        private bool CanUseCoyote => _coyoteUsable && !_ground.IsTouched && TimeLeftGrounded + _coyoteTime > Time.time;
        private bool IsGrounded => _ground.IsTouched;
        private bool IsJumpPressed => _direction.y > 0;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }
        private void FixedUpdate()
        {
            float directionX = CalculateVelocityX();
            float directionY = CalculateVelocityY();
            //_animatorController.SetIsGrounded(IsGrounded);
            //_animatorController.SetIsRunning(directionX != 0);

            _rb.velocity = new(directionX, directionY);
        }
        public void OnEnterPlatform(GameObject _object)
        {
            if (_object.CompareTag("Platform"))
            {
                _currentPlatforms.Add(_object.GetComponent<Collider2D>());
            }
        }
        public void OnExitPlatform(GameObject _object)
        {
            if (_object.CompareTag("Platform"))
            {
                _currentPlatforms.Remove(_object.GetComponent<Collider2D>());
            }
        }
        public void SetDirection(Vector2 vector)
        {
            float _directionX = vector.x;
            if (_directionX > 0)
            {
                transform.localScale = Vector3.one;
            }
            else if (_directionX < 0)
            {
                transform.localScale = new(-1, 1, 1);
            }
            _direction = new(_directionX, vector.y);
            _direction.Normalize();
        }
        private float CalculateVelocityX()
        {
            float InputX = _direction.x;
            float _currentHorizontalSpeed = _rb.velocity.x;
            if (InputX != 0)
            {
                _currentHorizontalSpeed += InputX * _acceleration * Time.deltaTime;
                _currentHorizontalSpeed = Mathf.Clamp(_currentHorizontalSpeed, -_moveClamp, _moveClamp);
            }
            else
            {
                // Инерция движения в падении
                _currentHorizontalSpeed = Mathf.MoveTowards(_currentHorizontalSpeed, 0, _deAcceleration * Time.deltaTime);
            }
            return _currentHorizontalSpeed;
        }
        private float CalculateVelocityY()
        {
            if (IsGrounded)
            {
                _countDoubleJump = _countDoubleJumpMax;
                _coyoteUsable = true;
            }

            float velocityY = _rb.velocity.y;

            if (IsJumpPressed)
            {
                if (_jumpCooldown.IsReady && (_countDoubleJump > 0 || (IsGrounded || CanUseCoyote)) && velocityY <= 0)
                {
                    velocityY = _jumpHeight;
                    _jumpCooldown.Reset();

                    if (!IsGrounded)
                    {
                        _countDoubleJump--;
                    }

                    _coyoteUsable = false;
                }
            }
            if (velocityY > 0 && !IsJumpPressed)
            {
                velocityY += Physics2D.gravity.y * (_lowJumpMultiplier - 1) * Time.deltaTime;
            }
            else if (velocityY < 0)
            {
                velocityY += Physics2D.gravity.y * (_fallMultiplier - 1) * Time.deltaTime;
            }
            if (_direction.y < 0 && _currentPlatforms.Count > 0)
            {
                StartCoroutine(DisablePlatformCollision());
            }
            //_animatorController.SetFloatVerticalVelocity(velocityY);
            //_animatorController.SetIsJumping(!IsGrounded && velocityY > 1);

            return velocityY;
        }
        private IEnumerator DisablePlatformCollision()
        {
            Collider2D playerCollider = GetComponent<Collider2D>();
            List<Collider2D> _listIgnore = new();

            foreach (Collider2D _collider in _currentPlatforms)
            {
                Physics2D.IgnoreCollision(playerCollider, _collider);
                _listIgnore.Add(_collider);
            }
            _platformCollision.Reset();
            while (!_platformCollision.IsReady)
            {
                yield return null;
            }
            foreach (Collider2D _collider in _listIgnore)
            {
                Physics2D.IgnoreCollision(playerCollider, _collider, false);
            }
        }
    }

    
}
