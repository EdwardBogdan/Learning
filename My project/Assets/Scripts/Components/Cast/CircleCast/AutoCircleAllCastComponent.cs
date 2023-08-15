using MyProject.Utils;
using UnityEditor;
using UnityEngine;

namespace MyProject.Components
{
    public class AutoCircleAllCastComponent : MonoBehaviour
    {
        [SerializeField] private float _radius;
        [SerializeField] private LayerMask _layer;
        [SerializeField] private UnityEvent_GameObject _action;

        private void Update()
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, _radius, _layer);
            if (_action == null) return;

            foreach (Collider2D hit in hits)
            {
                _action.Invoke(hit != null ? hit.gameObject : null);
            }
        }
        private void OnDrawGizmosSelected()
        {
            Handles.color = HandlesUtils.TransporentColorBlue;
            Handles.DrawSolidDisc(transform.position, Vector3.forward, _radius);
        }
    }
}
