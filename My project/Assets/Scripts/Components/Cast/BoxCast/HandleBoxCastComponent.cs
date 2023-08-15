using MyProject.Utils;
using UnityEditor;
using UnityEngine;

namespace MyProject.Components
{
    public class HandleBoxCastComponent : MonoBehaviour
    {
        [SerializeField] Vector2 _CheckSize;
        [SerializeField] LayerMask _layer;
        [SerializeField] UnityEvent_GameObject _action;
        public void Cast()
        {
            Collider2D hit = Physics2D.OverlapBox(transform.position, _CheckSize, 0f, _layer);
            _action?.Invoke(hit?.gameObject);
        }
        private void OnDrawGizmosSelected()
        {
            Handles.color = HandlesUtils.TransporentColorRed;
            Handles.DrawWireCube(transform.position, _CheckSize);
        }
    }
}
