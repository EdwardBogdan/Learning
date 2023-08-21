using MyProject.Components;
using UnityEngine;

namespace MyProject.Props
{
    public class Barrel : MonoBehaviour
    {
        [SerializeField] GroundCheckComponent _ground;
        [SerializeField] AutoBoxAllCastComponent _fallHitCast;        

        private void Update()
        {
            if (!_ground.isGrounded)
            {
                _fallHitCast.enabled = true;
            }
        }
    }
}
