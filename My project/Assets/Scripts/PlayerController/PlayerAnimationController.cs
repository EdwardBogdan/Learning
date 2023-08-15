using UnityEditor.Animations;
using UnityEngine;

namespace MyProject.Player
{
    public class PlayerAnimationController : MonoBehaviour
    {
        [SerializeField] AnimatorController _disarmedAnimator;
        [SerializeField] AnimatorController _armedAnimator;

        bool isArmed;

        Animator _animator;

        static readonly int animatorKey_IsGrounded = Animator.StringToHash("IsGrounded");
        static readonly int animatorKey_IsRunning = Animator.StringToHash("IsRunning");
        static readonly int animatorKey_IsJumping = Animator.StringToHash("IsJumping");
        static readonly int animatorKey_Hit = Animator.StringToHash("Hit");
        static readonly int animatorKey_DeathHit = Animator.StringToHash("Death Hit");
        static readonly int animatorKey_AttackSimple = Animator.StringToHash("AttackSimple");
        static readonly int animatorKey_VerticalVelocity = Animator.StringToHash("Vertical-Velocity");

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            SetArming(false);
        }
        public void SetArming(bool _value)
        {
            isArmed = _value;
            _animator.runtimeAnimatorController = _value ? _armedAnimator : _disarmedAnimator;
        }

        #region keysBool
        public void IsGrounded(bool _value)
        {
            _animator.SetBool(animatorKey_IsGrounded, _value);
        }
        public void IsRunning(bool _value)
        {
            _animator.SetBool(animatorKey_IsRunning, _value);
        }
        public void IsJumping(bool _value)
        {
            _animator.SetBool(animatorKey_IsJumping, _value);
        }
        #endregion

        #region keysTriggers
        public void TriggerHit()
        {
            _animator.SetTrigger(animatorKey_Hit);
        }
        public void TriggerAttackSimple()
        {
            if (!isArmed) return;

            _animator.SetTrigger(animatorKey_AttackSimple);
        }
        public void TriggerDeathHit()
        {
            _animator.SetTrigger(animatorKey_DeathHit);
        }
        #endregion

        #region keyFloat
        public void VerticalVelocity(float _value)
        {
            _animator.SetFloat(animatorKey_VerticalVelocity, _value);
        }
        #endregion
    }
}
