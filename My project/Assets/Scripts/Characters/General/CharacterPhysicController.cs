using MyProject.Characters;
using MyProject.Components;
using MyProject.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyProject.Characters
{
    public class CharacterPhysicController : MonoBehaviour
    {
        //Value
        [SerializeField] protected float _speed;
        [SerializeField] protected float _jumpSpeed;
        [SerializeField] protected Cooldown _jumpCooldown;
        [SerializeField] protected Cooldown _platformCollision;

        //Components
        [SerializeField] protected TouchComponent _ground;
        [SerializeField] protected TouchComponent _wall;

        protected Rigidbody2D _rigidbody;
        protected GeneralAnimationController _animatorController;

        protected List<Collider2D> _currentPlatforms = new();

        protected Vector2 _direction;
        public Vector2 GetDirection => _direction;
        protected Vector3 ÑurrentPosition => transform.position;
        protected bool IsGrounded => _ground.IsTouched;
        protected virtual bool IsJumpPressed => _direction.y > 0;

        protected virtual void Awake()
        {
            _animatorController = GetComponent<GeneralAnimationController>();
            _rigidbody = GetComponent<Rigidbody2D>();
        }
        protected virtual void FixedUpdate()
        {
            float directionX = CalculateVelocityX();
            float directionY = CalculateVelocityY();
            _animatorController.SetIsGrounded(IsGrounded);
            _animatorController.SetIsRunning(directionX != 0);

            _rigidbody.velocity = new(directionX * _speed, directionY);
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
            float directionX = _direction.x;
            if (directionX != 0 && !IsGrounded && _wall.IsTouched)
            {
                directionX = 0;
            }
            return directionX;
        }
        private float CalculateVelocityY()
        {
            float velocity_Y = _rigidbody.velocity.y;

            if (IsJumpPressed)
            {
                velocity_Y = CalculateJump(velocity_Y);
            }
            else if (velocity_Y > 0)
            {
                velocity_Y *= 0.5f;
            }
            else if (_direction.y < 0 && _currentPlatforms.Count > 0)
            {
                StartCoroutine(DisablePlatformCollision());
            }
            _animatorController.SetFloatVerticalVelocity(velocity_Y);
            _animatorController.SetIsJumping(!IsGrounded && velocity_Y > 1);

            return velocity_Y;
        }
        protected virtual float CalculateJump(float velocity_Y)
        {
            if (IsGrounded && velocity_Y <= 0 && _jumpCooldown.IsReady)
            {
                velocity_Y = _jumpSpeed;
                _jumpCooldown.Reset();
            }
            return velocity_Y;
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
