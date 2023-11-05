using CustomUnityUtils.TimeUtils;
using UnityEngine;
using ColliderTouchSystem;
using PhysicModuleSystem;

namespace MyProject.Physic.Modules
{
    public class PM_SlimeJumping : PhysicModule
    {
        [SerializeField] private string _groundId;

        [SerializeField] private float _jumpHeight;

        [SerializeField] private Cooldown _jumpFrequency;

        public override string ModuleName => "Slime jumping at Y axis";

        private TouchComponent _ground;

        private PhysicModuleController _controller;

        public override void Activating(PhysicModuleController controller)
        {
            _controller = controller;
            _jumpFrequency.Reset();
            _ground = controller.GetTouchProperty(_groundId);
        }
        public override void Modification()
        {
            if (_jumpFrequency.IsReady && _ground.IsTouched)
            {
                _jumpFrequency.Reset();

                _controller.RigidBody.velocity = new(_controller.Velocity.x, _jumpHeight);
            }
        }
    }
}
