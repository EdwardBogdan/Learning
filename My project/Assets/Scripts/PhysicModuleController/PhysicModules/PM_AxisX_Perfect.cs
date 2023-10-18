using UnityEngine;

namespace MyProject.Physic.Modules
{
    public class PM_AxisX_Perfect : PhysicModule
    {
        [SerializeField] private float _acceleration;
        [SerializeField] private float _moveClamp;
        [SerializeField] private float _deAcceleration;

        public override string ModuleName => "WALKING";

        public override Vector2 Modification(ControllDataPack data)
        {
            float InputX = data.directionData.x;
            float _currentHorizontalSpeed = data.velocityData.x;

            if (InputX != 0)
            {
                _currentHorizontalSpeed += InputX * _acceleration * Time.deltaTime;
                _currentHorizontalSpeed = Mathf.Clamp(_currentHorizontalSpeed, -_moveClamp, _moveClamp);
            }
            else
            {
                // Инерция движения
                _currentHorizontalSpeed = Mathf.MoveTowards(_currentHorizontalSpeed, 0, _deAcceleration * Time.deltaTime);
            }

            return new(_currentHorizontalSpeed, data.velocityData.y);
        }
    }
}
