using UnityEngine;

namespace PhysicComponents
{
    public class Rigidbody2DConstraintsController : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rb;
        public void FreezeConstraintY(bool isFrozen)
        {
            var currentConstraints = _rb.constraints;

            if (isFrozen)
            {
                currentConstraints &= ~RigidbodyConstraints2D.FreezePositionY;
            }
            else
            {
                currentConstraints |= RigidbodyConstraints2D.FreezePositionY;
            }

            _rb.constraints = currentConstraints;
        }
    }
}
