using MyProject.Components;
using UnityEngine;

namespace MyProject.Characters
{
    public class PlayerPhysicController : CharacterPhysicController
    {
        [SerializeField][Range(0, 5)] int _countDoubleJumpMax;
        GravitationComponent _gravity;

        private int _countDoubleJump;

        protected override void Awake()
        {
            _gravity = GetComponent<GravitationComponent>();
            base.Awake();
        }
        protected override float CalculateJump(float velocity)
        {
            if (IsGrounded)
            {
                _countDoubleJump = _countDoubleJumpMax;
            }

            if (_jumpCooldown.IsReady && (_countDoubleJump > 0 || IsGrounded) && velocity <= 0)
            {
                velocity = _jumpSpeed;
                _jumpCooldown.Reset();

                if (!IsGrounded)
                {
                    _countDoubleJump--;
                    _gravity.DropSpeed();
                }
            }

            return velocity;
        }
    }
}
