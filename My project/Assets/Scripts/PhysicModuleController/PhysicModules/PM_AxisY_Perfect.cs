using UnityEngine;
using MyProject.Utils;
using System;
using MyProject.Components.Triggers;

namespace MyProject.Physic.Modules
{
    [Serializable]
    public class PM_AxisY_Perfect : PhysicModule
    {
        [SerializeField] private float _jumpHeight;
        [SerializeField][Range(0, 5)] int _countDoubleJumpMax;
        [SerializeField] private float _coyoteTime;
        [SerializeField] private float _fallMultiplier;
        [SerializeField] private float _lowJumpMultiplier;
        [SerializeField] protected Cooldown _jumpCooldown;

        public override string ModuleName => "JUMPING";

        private int _countDoubleJump;

        private bool _coyoteUsable;

        private TouchComponent ground;

        private bool Grounded => ground.IsTouched;
        private float TimeLeftGrounded => ground.LastTouchTime;
        private bool CanUseCoyote => _coyoteUsable && !Grounded && TimeLeftGrounded + _coyoteTime > Time.time;

        public override void Activating(ControllDataPack data)
        {
            _jumpCooldown.Reset();

            ground = data.groundData;
        }
        public override Vector2 Modification(ControllDataPack data)
        {
            if (data.groundData.IsTouched)
            {
                _countDoubleJump = _countDoubleJumpMax;
                _coyoteUsable = true;
            }

            float velocityY = data.velocityData.y;

            bool IsJumpPressed = data.directionData.y > 0;

            if (IsJumpPressed && _jumpCooldown.IsReady && velocityY <= 0)
            {
                if (Grounded || CanUseCoyote)
                {
                    velocityY = _jumpHeight;
                }
                else if (_countDoubleJump > 0)
                {
                    velocityY = _jumpHeight;
                    _countDoubleJump--;
                }

                _jumpCooldown.Reset();

                _coyoteUsable = false;
            }
            else if (velocityY > 0 && !IsJumpPressed)
            {
                velocityY += Physics2D.gravity.y * (_lowJumpMultiplier - 1) * Time.deltaTime;
            }
            else if (velocityY < 0)
            {
                velocityY += Physics2D.gravity.y * (_fallMultiplier - 1) * Time.deltaTime;
            }

            return new(data.velocityData.x, velocityY);
        }
    }
}