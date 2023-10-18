using MyProject.Utils;
using UnityEngine;

namespace MyProject.Components.Cast
{
    public class BoxCastComponent : CastComponent
    {
        [SerializeField] protected Vector2 _CheckSize;
        public override string NameElement => $"B.Cast" + base.NameElement;
        public override void Cast()
        {
            Collider2D hit = Physics2D.OverlapBox(GetNewPos(), _CheckSize, 0f, _layer);
            _action?.Invoke(hit != null ? hit.gameObject : null);
        }
        protected override void OnDrawGizmosSelected()
        {
            Gizmos.color = ColorStore.GetColor(_color);
            Gizmos.DrawCube(GetNewPos(), new(_CheckSize.x,_CheckSize.y, 0));
        }
    }
}
