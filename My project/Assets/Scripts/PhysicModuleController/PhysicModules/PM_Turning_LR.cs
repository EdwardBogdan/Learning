using UnityEngine;

namespace MyProject.Physic.Modules
{
    public class PM_Turning_LR : PhysicModule
    {
        public override string ModuleName => "TURNING LEFT - RIGHT";

        private Vector3 _scale;
        public override Vector2 Modification(ControllDataPack data)
        {
            if (data.directionData.x > 0)
            {
                data.body.transform.localScale = _scale;
            }
            else if (data.directionData.x < 0)
            {
                data.body.transform.localScale = new(-1 * _scale.x, _scale.y, _scale.z);
            }
            return data.velocityData;
        }
        public override void Activating(ControllDataPack data)
        {
            base.Activating(data);
            _scale = data.body.transform.localScale;
        }
    }
}
