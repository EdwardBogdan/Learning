
using MyProject.Components.Triggers;
using MyProject.Utils;
using UnityEngine;

namespace MyProject.Physic.Modules
{
    public class PM_Wall_Sliding : PhysicModule
    {
        [SerializeField] float _speedLimit;
        [SerializeField] float _factor;
        [SerializeField] Cooldown _frequency;

        [SerializeField] bool _active;

        public override string ModuleName => "WALL SLIDING (Doesnt work)";

        TouchComponent _wall;
        TouchComponent _ground;
        private bool Walled => _wall.IsTouched;
        private bool Grounded => _ground.IsTouched;
        public override void Activating(ControllDataPack data)
        {
            _wall = data.wallData;
            _ground = data.groundData;
        }
        public override Vector2 Modification(ControllDataPack data)
        {
            Vector2 velocity = data.velocityData;

            if (Walled && _frequency.IsReady && _active)
            {
                _frequency.Reset();

                float velocityX = velocity.x;

                if (velocityX >= _speedLimit)
                {
                    velocityX = velocityX < 0 ? -velocityX : velocityX;

                    velocity = new(velocity.x, velocityX * _factor + velocity.y);
                }
            }

            return velocity;
        }
    }
}
