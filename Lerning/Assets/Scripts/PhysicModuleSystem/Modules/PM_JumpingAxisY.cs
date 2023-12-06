using UnityEngine;
using System;
using ColliderTouchSystem;
using CustomUnityUtils.TimeUtils;

namespace PhysicModuleSystem
{
    public class PM_JumpingAxisY : PhysicModule
    {
        [SerializeField] private string _groundId;
        [SerializeField] private float _jumpHeight;
        [SerializeField] private float _doubleJumpHeight;
        [SerializeField][Range(0, 5)] int _countDoubleJumpMax;
        [SerializeField] private float _coyoteTime;
        [SerializeField] private float _fallMultiplier;
        [SerializeField] private float _lowJumpMultiplier;
        [SerializeField] protected Cooldown _jumpCooldown;

        public override string ModuleName => "Jumping at Y axis";

        private int _countDoubleJump;

        private bool _coyoteUsable;

        private TouchComponent _ground;

        private PhysicModuleController _controller;

        private bool Grounded => _ground.IsTouched;
        private float TimeLeftGrounded => _ground.LastTouchTime;
        private bool CanUseCoyote => _coyoteUsable && !Grounded && TimeLeftGrounded + _coyoteTime > Time.time;

        public override void Activating(PhysicModuleController controller)
        {
            _jumpCooldown.Reset();

            _controller = controller;
            _ground = controller.GetTouchProperty(_groundId);
        }
        public override void Modification()
        {
            if (_ground.IsTouched)
            {
                _countDoubleJump = _countDoubleJumpMax;
                _coyoteUsable = true;
            }

            float velocityY = _controller.Velocity.y;

            bool IsJumpPressed = _controller.Direction.y > 0;

            if (IsJumpPressed && _jumpCooldown.IsReady && velocityY <= 0)
            {
                if (Grounded || CanUseCoyote)
                {
                    velocityY = _jumpHeight;

                    SetJump(velocityY);
                }
                else if (_countDoubleJump > 0)
                {
                    velocityY = _doubleJumpHeight;

                    SetJump(velocityY);

                    _countDoubleJump--;
                }

                _jumpCooldown.Reset();

                _coyoteUsable = false;
            }
            else if (velocityY > 0 && !IsJumpPressed)
            {
                velocityY += Physics2D.gravity.y * (_lowJumpMultiplier - 1) * Time.deltaTime;

                SetJump(velocityY);
            }
            else if (velocityY < 0)
            {
                velocityY += Physics2D.gravity.y * (_fallMultiplier - 1) * Time.deltaTime;

                SetJump(velocityY);
            }

            
        }

        private void SetJump(float velocityY)
        {
            _controller.RigidBody.velocity = new(_controller.Velocity.x, velocityY);
        }
    }
}