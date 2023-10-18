using MyProject.Components.Triggers;
using UnityEngine;
using MyProject.Utils;

namespace MyProject.Physic.Modules
{
    public class PM_SlimeJumping : PhysicModule
    {
        [SerializeField] private float _jumpHeight;

        [SerializeField] private Cooldown _jumpFrequency;

        public override string ModuleName => "Slime Jumping";

        private TouchComponent ground;
        private bool Grounded => ground.IsTouched;

        public override void Activating(ControllDataPack data)
        {
            _jumpFrequency.Reset();
            ground = data.groundData;
        }
        public override Vector2 Modification(ControllDataPack data)
        {
            float velocityY = data.velocityData.y;

            if (_jumpFrequency.IsReady && Grounded)
            {
                _jumpFrequency.Reset();
                velocityY = _jumpHeight;
            }

            return new(data.velocityData.x, velocityY);
        }
    }
}
