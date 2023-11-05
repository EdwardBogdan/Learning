using UnityEngine;

namespace PhysicModuleSystem
{
    internal class PM_MovingAxisX : PhysicModule
    {
        [SerializeField] private float _acceleration;
        [SerializeField] private float _moveClamp;
        [SerializeField] private float _deAcceleration;


        private PhysicModuleController _controller;
        private Vector2 Direction => _controller.Direction;
        private Vector2 Velocity => _controller.Velocity;
        public override string ModuleName => "Moving at X axis";

        private float currentHorizontalSpeed;
        private float inputX;

        public override void Activating(PhysicModuleController controller)
        {
            _controller = controller;
        }

        public override void Modification()
        {
            inputX = Direction.x;
            currentHorizontalSpeed = Velocity.x;

            if (inputX != 0)
            {
                currentHorizontalSpeed += inputX * _acceleration * Time.deltaTime;
                currentHorizontalSpeed = Mathf.Clamp(currentHorizontalSpeed, -_moveClamp, _moveClamp);
            }
            else
            {
                // Инерция движения
                currentHorizontalSpeed = Mathf.MoveTowards(currentHorizontalSpeed, 0, _deAcceleration * Time.deltaTime);
            }

            _controller.RigidBody.velocity = new(currentHorizontalSpeed, Velocity.y);
        }
    }
}
