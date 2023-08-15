using MyProject.Utils;
using UnityEditor;
using UnityEngine;

namespace MyProject.Components
{
    public class HandleBoxAllCastComponent : MonoBehaviour
    {
        [SerializeField] Vector2 _CheckSize;
        [SerializeField] LayerMask _layer;
        [SerializeField] UnityEvent_GameObject _action;
        public void Cast()
        {
            Collider2D[] hits = Physics2D.OverlapBoxAll(transform.position, _CheckSize, 0f, _layer);

            if (_action == null) return;

            foreach (Collider2D hit in hits)
            {
                _action.Invoke(hit != null ? hit.gameObject : null);
            }
        }
        private void OnDrawGizmosSelected()
        {
            Handles.color = HandlesUtils.TransporentColorBlue;
            Handles.DrawWireCube(transform.position, _CheckSize);
        }
    }
}
