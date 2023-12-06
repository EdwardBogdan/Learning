using UnityEngine;

namespace PhysicComponents
{
    public class ExplosionComponent : MonoBehaviour
    {
        [SerializeField] private float _explosiontForceMulti;

        public void Explode(GameObject _object)
        {
            Collider2D collider = _object.GetComponent<Collider2D>();
            if (collider.TryGetComponent(out Rigidbody2D rb))
            {
                Vector2 distanceVector = collider.transform.position - transform.position;

                if (distanceVector.magnitude > 0)
                {
                    float explosionForce = _explosiontForceMulti / distanceVector.magnitude;
                    rb.AddForce(distanceVector.normalized * explosionForce);
                }
            }
        }
    }
}
