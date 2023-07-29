using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace MyProject.Player
{
    public class PlayerControler : MonoBehaviour
    {
        [SerializeField] float _speed;
        [SerializeField] float _jumpSpeed;

        Rigidbody2D _rigidbody;
        Animator _animator;
        SpriteRenderer _spriteRenderer;

        Vector2 _direction;

        public bool isGrounded;
        public bool isHitWallLeft;
        public bool isHitWallRight;

        static readonly int keyIsGrounded = Animator.StringToHash("IsGrounded");
        static readonly int keyIsRunning = Animator.StringToHash("IsRunning");
        static readonly int keyIsJumping = Animator.StringToHash("IsJumping");
        static readonly int keyDeadGround = Animator.StringToHash("Dead Ground");
        static readonly int keyHit = Animator.StringToHash("Hit");
        static readonly int keyDeadHit = Animator.StringToHash("Dead Hit");
        static readonly int keyVerticalVelocity = Animator.StringToHash("Vertical-Velocity");

        public void SetDirection(Vector2 vector)
        {
            _direction = vector;
        }
        public Vector2 GetDirection()
        {
            return _direction;
        }
        void Move()
        {
            float directionX = _direction.x;
            if (!isGrounded && ((directionX < 0 && isHitWallLeft) || (directionX > 0 && isHitWallRight)))
            {
                directionX = 0;
            }

            _rigidbody.velocity = new(directionX * _speed, _rigidbody.velocity.y);

            SetFlip(directionX);

            _animator.SetBool(keyIsGrounded, isGrounded);
            _animator.SetBool(keyIsRunning, directionX != 0);
        }
        void Jump()
        {
            bool isJumping = _direction.y > 0;
            if (isJumping)
            {
                if (isGrounded && _rigidbody.velocity.y <= 0)
                {
                    _rigidbody.velocity = new(_rigidbody.velocity.x, 0);
                    _rigidbody.AddForce(Vector2.up * _jumpSpeed, ForceMode2D.Impulse);
                }
            }
            else if (_rigidbody.velocity.y > 0)
            {
                _rigidbody.velocity = new(_rigidbody.velocity.x, _rigidbody.velocity.y * 0.5f);
            }
            _animator.SetFloat(keyVerticalVelocity, _rigidbody.velocity.y);
            _animator.SetBool(keyIsJumping, isJumping && _rigidbody.velocity.y > 0);
        }
        void SetFlip(float direction)
        {
            if (direction > 0)
            {
                _spriteRenderer.flipX = false;
            }
            else if (direction < 0)
            {
                _spriteRenderer.flipX = true;
            }
        }
        private void FixedUpdate()
        {
            Move();
            Jump();
        }
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }
    }
}
