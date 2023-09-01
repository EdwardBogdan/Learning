using MyProject.Utils;
using UnityEditor;
using UnityEngine;

namespace MyProject.Components
{
    public class BoxCastComponent : CircleAllCastComponent
    {
        [SerializeField] protected Vector2 _CheckSize;
        public override string NameElement => $"B.Cast" + base.NameElement;
        public override void Cast()
        {
            Collider2D hit = Physics2D.OverlapBox(transform.position, _CheckSize, 0f, _layer);
            _action?.Invoke(hit != null ? hit.gameObject : null);
        }
        protected override void OnDrawGizmosSelected()
        {
            Gizmos.color = ColorStore.GetColor(_color);
            Gizmos.DrawCube(transform.position, new(_CheckSize.x,_CheckSize.y, 0));
        }
    }
}
