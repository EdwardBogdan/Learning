using UnityEngine;

namespace MyProject.Physic.Modules
{
    public class PM_SlimeMoving : PhysicModule
    {
        [SerializeField] private float _acceleration;
        [SerializeField] private float _moveClamp;

        public override string ModuleName => "Slime Walking";

        public override Vector2 Modification(ControllDataPack data)
        {
            if (data.groundData.IsTouched) return new(0, data.velocityData.y);

            float InputX = data.directionData.x;
            float _currentHorizontalSpeed = data.velocityData.x;

            if (InputX != 0)
            {
                _currentHorizontalSpeed += InputX * _acceleration * Time.deltaTime;
                _currentHorizontalSpeed = Mathf.Clamp(_currentHorizontalSpeed, -_moveClamp, _moveClamp);
            }

            return new(_currentHorizontalSpeed, data.velocityData.y);
        }
    }
}
