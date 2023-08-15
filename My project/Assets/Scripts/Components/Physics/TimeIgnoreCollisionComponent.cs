using UnityEngine;
using System.Collections;

namespace MyProject.Components
{
    public class TimeIgnoreCollisionComponent : MonoBehaviour
    {
        [SerializeField] float _ignoreTime = 0.25f;
        public void GetIgnored(GameObject _object)
        {
            StartCoroutine(DisableCollision(_object));
        }
        private IEnumerator DisableCollision(GameObject _object)
        {
            Collider2D thisCollider = GetComponent<Collider2D>();
            Collider2D _objectCollider = _object.GetComponent<Collider2D>();
            Physics2D.IgnoreCollision(thisCollider, _objectCollider);
            yield return new WaitForSeconds(_ignoreTime);
            Physics2D.IgnoreCollision(thisCollider, _objectCollider, false);
        }
    }
}
