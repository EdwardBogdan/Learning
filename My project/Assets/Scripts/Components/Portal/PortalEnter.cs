using UnityEngine;

namespace MyProject.Components.Portal
{
    public class PortalEnter : PortalExit
    {
        [SerializeField] PortalExit _exitPortal;
        [SerializeField] float _pushForce = 100f;

        public void OnTeleport(GameObject _object)
        {
            _object.transform.position = _exitPortal._exitPoint.position;
            Rigidbody2D rb = _object.GetComponent<Rigidbody2D>();
            Vector2 direction = _exitPortal._exitPoint.transform.position - _exitPortal.transform.position;
            rb.velocity = new(0, 0);
            rb.AddForce(direction * _pushForce, ForceMode2D.Impulse);
            var v = rb.velocity;
        }
        private void OnDrawGizmos()
        {
            if (_exitPortal != null)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawLine(transform.position, _exitPortal.transform.position);
            }
            else
            {
                Gizmos.color = Color.cyan;
                Gizmos.DrawCube(transform.position, new(0.3f,0.3f,0));
            }
            
        }
    }
}