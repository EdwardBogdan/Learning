using ColliderTouchSystem;
using UnityEngine;

namespace PhysicModuleSystem
{
    public class PM_SlimeMoving : PhysicModule
    {
        [SerializeField] private string _groundId;
        [SerializeField] private float _acceleration;
        [SerializeField] private float _moveClamp;

        private TouchComponent _ground;

        private PhysicModuleController _controller;

        public override string ModuleName => "Slime Walking at X axis";


        public override void Activating(PhysicModuleController controller)
        {
            _controller = controller;
            _ground = controller.GetTouchProperty(_groundId);
        }
        public override void Modification()
        {
            if (_ground.IsTouched) return;

            float InputX = _controller.Direction.x;

            if (InputX != 0)
            {
                float _currentHorizontalSpeed = _controller.Velocity.x;

                _currentHorizontalSpeed += InputX * _acceleration * Time.deltaTime;
                _currentHorizontalSpeed = Mathf.Clamp(_currentHorizontalSpeed, -_moveClamp, _moveClamp);

                _controller.RigidBody.velocity = new(_currentHorizontalSpeed, _controller.Velocity.y);
            }
        }
    }
}
