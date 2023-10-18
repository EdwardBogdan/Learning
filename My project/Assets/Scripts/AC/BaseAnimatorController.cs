using MyProject.Components.Triggers;
using MyProject.Physic.Modules;
using MyProject.Utils;
using UnityEngine;

namespace MyProject.Physic.PAController
{
    public class BaseAnimatorController : MonoBehaviour
    {
        [SerializeField] protected Cooldown _attackInterval;
        [SerializeField] protected Cooldown _throwInterval;

        protected Animator _controller;
        protected ControllDataPack _data;
        protected TouchComponent _ground;
        protected bool IsGrounded => _ground.IsTouched;
        protected bool IsRunning => _data.velocityData.x != 0;
        protected float VerticalVelocity => _data.velocityData.y;

        protected static readonly int animatorKey_IsGrounded = Animator.StringToHash("IsGrounded");
        protected static readonly int animatorKey_IsRunning = Animator.StringToHash("IsRunning");

        protected static readonly int animatorKey_Hit = Animator.StringToHash("Hit");
        protected static readonly int animatorKey_Attack = Animator.StringToHash("Attack");
        protected static readonly int animatorKey_Ground = Animator.StringToHash("Ground");
        protected static readonly int animatorKey_Throw = Animator.StringToHash("Throw");

        protected static readonly int animatorKey_VerticalVelocity = Animator.StringToHash("Vertical Velocity");

        protected void Awake()
        {
            _controller = GetComponent<Animator>();
        }
        protected virtual void Start()
        {
            _data = GetComponent<PhysicModuleController>().Data;
            _ground = _data.groundData;
        }
        protected virtual void FixedUpdate()
        {
            _controller.SetBool(animatorKey_IsGrounded, IsGrounded);
            _controller.SetBool(animatorKey_IsRunning, IsRunning);
            _controller.SetFloat(animatorKey_VerticalVelocity, VerticalVelocity);
        }
        public void ACTriggerHit()
        {
            _controller.SetTrigger(animatorKey_Hit);
        }
        public virtual void ACTriggerAttack()
        {
            if (!_attackInterval.IsReady) return;

            _attackInterval.Reset();
            _controller.SetTrigger(animatorKey_Attack);
        }
        public void ACTriggerGround()
        {
            _controller.SetTrigger(animatorKey_Ground);
        }
        public virtual void ACTriggerThrow()
        {
            if (!_throwInterval.IsReady) return;

            _controller.SetTrigger(animatorKey_Throw);
        }
    }
}
