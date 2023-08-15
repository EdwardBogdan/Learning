using MyProject.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyProject.Player
{
    public class PlayerPhysicController : MonoBehaviour
    {
        [SerializeField] float _speed;
        [SerializeField] float _jumpSpeed;
        [SerializeField] [Range(0,3)] int _countDoubleJumpMax;

        int _countDoubleJump;

        bool _isGrounded => _ground.isGrounded;
        bool _isHitWallLeft;
        bool _isHitWallRight;

        Vector2 _direction;

        //Components
        [SerializeField] GroundCheckComponent _ground;
        Rigidbody2D _rigidbody;
        PlayerAnimationController _playerAnimator;
        GravitationComponent _gravity;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _playerAnimator = GetComponent<PlayerAnimationController>();
            _gravity = GetComponent<GravitationComponent>();
        }
        void FixedUpdate()
        {
            float directionX = CalculateVelocityX();
            float directionY = CalculateVelocityY();
            _rigidbody.velocity = new(directionX * _speed, directionY);
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
            _playerAnimator.IsRunning(_directionX != 0);
            _direction = new(_directionX, vector.y);
        }
        public Vector2 GetDirection()
        {
            return _direction;
        }
        public void SetFlags(bool isHeitWallLeft, bool isHitWallRight)
        {
            _isHitWallLeft = isHeitWallLeft;
            _isHitWallRight = isHitWallRight;

            _playerAnimator.IsGrounded(_isGrounded);
        }
        float CalculateVelocityX()
        {
            float directionX = _direction.x;
            if (!_isGrounded && ((directionX < 0 && _isHitWallLeft) || (directionX > 0 && _isHitWallRight)))
            {
                directionX = 0;
            }
            return directionX;
        }
        float CalculateVelocityY()
        {
            float velocityY = _rigidbody.velocity.y;
            bool isJumpPressing = _direction.y > 0;

            if (_isGrounded) _countDoubleJump = _countDoubleJumpMax;

            if (isJumpPressing)
            {
                velocityY = CalculateJump(velocityY);
            }
            else if (velocityY > 0)
            {
                velocityY *= 0.5f;
            }
            _playerAnimator.VerticalVelocity(_rigidbody.velocity.y);
            _playerAnimator.IsJumping(isJumpPressing && velocityY > 1);

            return velocityY;

            float CalculateJump(float velocity)
            {
                if (velocity > 0) return velocity;

                if (_isGrounded && velocity <= 0)
                {
                    velocity = _jumpSpeed;
                }
                else if (_countDoubleJump > 0)
                {
                    velocity = _jumpSpeed;
                    _countDoubleJump--;
                    _gravity.Reseting();
                }
                return velocity;
            }
        }
    }
}
