using MyProject.Utils;
using UnityEditor;
using UnityEngine;

namespace MyProject.Components
{
    public class AutoBoxCastComponent : MonoBehaviour
    {
        [SerializeField] private Vector2 _CheckSize;
        [SerializeField] private LayerMask _layer;
        [SerializeField] private UnityEvent_GameObject _action;

        Collider2D _touchedElement;
        private void Update()
        {
            Collider2D hit = _touchedElement = Physics2D.OverlapBox(transform.position, _CheckSize, 0f, _layer);
            _action?.Invoke(hit != null ? hit.gameObject : null);
        }
        private void OnDrawGizmosSelected()
        {
            Handles.color = _touchedElement != null ? HandlesUtils.TransporentColorGreen : HandlesUtils.TransporentColorRed;
            Handles.DrawWireCube(transform.position, _CheckSize);
        }
    }
}
