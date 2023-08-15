using MyProject.Utils;
using UnityEditor;
using UnityEngine;

namespace MyProject.Components
{
    public class HandleCircleCastComponent : MonoBehaviour
    {
        [SerializeField] private float _radius;
        [SerializeField] private LayerMask _layer;
        [SerializeField] private UnityEvent_GameObject _action;
        public void Cast()
        {
            Collider2D hit = Physics2D.OverlapCircle(transform.position, _radius, _layer);
            _action?.Invoke(hit != null ? hit.gameObject : null);
        }
        private void OnDrawGizmosSelected()
        {
            Handles.color = HandlesUtils.TransporentColorRed;
            Handles.DrawSolidDisc(transform.position, Vector3.forward, _radius);
        }
    }
}
