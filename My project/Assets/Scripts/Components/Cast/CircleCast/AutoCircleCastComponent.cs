using MyProject.Utils;
using UnityEditor;
using UnityEngine;

namespace MyProject.Components
{
    public class AutoCircleCastComponent : MonoBehaviour
    {
        [SerializeField] private float _radius;
        [SerializeField] private LayerMask _layer;
        [SerializeField] private UnityEvent_GameObject _action;

        Collider2D _touchedElement;
        private void Update()
        {
            Collider2D hit = Physics2D.OverlapCircle(transform.position, _radius, _layer);
            _action?.Invoke(hit != null ? hit.gameObject : null);
        }
        private void OnDrawGizmosSelected()
        {
            Handles.color = _touchedElement != null ? HandlesUtils.TransporentColorGreen : HandlesUtils.TransporentColorRed;
            Handles.DrawSolidDisc(transform.position, Vector3.forward, _radius);
        }
    }
}
