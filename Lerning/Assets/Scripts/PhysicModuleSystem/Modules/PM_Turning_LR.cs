using UnityEngine;

namespace PhysicModuleSystem
{
    public class PM_Turning_LR : PhysicModule
    {
        public override string ModuleName => "Turning L-R";

        private Vector3 _scale;

        private PhysicModuleController _controller;
        public override void Modification()
        {
            float inputX = _controller.Direction.x;

            if (inputX > 0)
            {
                _controller.RigidBody.transform.localScale = _scale;
            }
            else if (inputX < 0)
            {
                _controller.RigidBody.transform.localScale = new(-1 * _scale.x, _scale.y, _scale.z);
            }
        }
        public override void Activating(PhysicModuleController controller)
        {
            _controller = controller;
            _scale = controller.RigidBody.transform.localScale;

            if (_scale.x < 0) _scale.x *= -1;
            if (_scale.y < 0) _scale.y *= -1;
            if (_scale.z < 0) _scale.z *= -1;
        }
    }
}
