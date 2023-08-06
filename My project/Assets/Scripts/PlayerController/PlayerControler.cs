using MyProject.Components;
using UnityEngine;

namespace MyProject.Player
{
    public class PlayerControler : MonoBehaviour
    {
        [SerializeField] float _speed;
        [SerializeField] float _jumpSpeed;
        [SerializeField] float _jumpSpeedDamage;
        [SerializeField] float _interactRadius;
        
        [SerializeField] LayerMask _interactionLayer;

        [SerializeField] ParticleSystem _particleSystem;

        Rigidbody2D _rigidbody;
        Animator _animator;

        Vector2 _direction;

        int _countDoubleJump = 2;

        bool _isGrounded;
        bool _isHitWallLeft;
        bool _isHitWallRight;

        Collider2D[] _interactionResult = new Collider2D[1];

        static readonly int keyIsGrounded = Animator.StringToHash("IsGrounded");
        static readonly int keyIsRunning = Animator.StringToHash("IsRunning");
        static readonly int keyIsJumping = Animator.StringToHash("IsJumping");
        static readonly int keyDeadGround = Animator.StringToHash("Dead Ground");
        static readonly int keyHit = Animator.StringToHash("Hit");
        static readonly int keyDeadHit = Animator.StringToHash("Dead Hit");
        static readonly int keyVerticalVelocity = Animator.StringToHash("Vertical-Velocity");

        private void OnDrawGizmos()
        {
            Gizmos.DrawSphere(transform.position, _interactRadius);
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
            _animator.SetBool(keyIsRunning, _directionX != 0);
            _direction = new(_directionX, vector.y);
        }
        public Vector2 GetDirection()
        {
            return _direction;
        }
        public void SetFlags(bool isGrounded, bool isHeitWallLeft, bool isHitWallRight)
        {
            _isGrounded = isGrounded;
            _isHitWallLeft = isHeitWallLeft;
            _isHitWallRight = isHitWallRight;

            _animator.SetBool(keyIsGrounded, _isGrounded);
        }
        public void TakeDamage()
        {
            _animator.SetTrigger(keyHit);
            _rigidbody.velocity = new(_rigidbody.velocity.x, _jumpSpeedDamage);
            Debug.Log($"HP: {GetComponent<HealthComponent>().Health}");

            DisposeCoin();
        }
        public void DisposeCoin()
        {
            var _numCoinToDispose = Mathf.Min(CoinTriger._count, 5);
            CoinTriger._count -= _numCoinToDispose;
            var burst = _particleSystem.emission.GetBurst(0);
            burst.count = _numCoinToDispose;
            _particleSystem.emission.SetBurst(0, burst);
            //_particleSystem.gameObject.SetActive(true);
            _particleSystem.Play();
        }
        public void Death()
        {
            Debug.Log("Умер, воскрес, но тебя, забыли");
        }
        public void Interact()
        {
            var size = Physics2D.OverlapCircleNonAlloc(transform.position, _interactRadius, _interactionResult, _interactionLayer);
            for (int x = 0; x < size; x++)
            {
                _interactionResult[x].GetComponent<InteractableComponent>()?.Interact();
            }
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

            if (_isGrounded) _countDoubleJump = 2;
            
            if (isJumpPressing)
            {
                velocityY = CalculateJump(velocityY);
            }
            else if (velocityY > 0)
            {
                velocityY *= 0.5f;
            }
            _animator.SetFloat(keyVerticalVelocity, _rigidbody.velocity.y);
            _animator.SetBool(keyIsJumping, isJumpPressing && velocityY > 0);

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
                }
                return velocity;
            }
        }
        void FixedUpdate()
        {
            float directionX = CalculateVelocityX();
            float directionY = CalculateVelocityY();
            _rigidbody.velocity = new(directionX * _speed, directionY);
        }
        void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
        }
    }
}
