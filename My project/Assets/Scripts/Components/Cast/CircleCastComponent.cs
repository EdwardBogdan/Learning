using MyProject.Utils;
using UnityEditor;
using UnityEngine;

namespace MyProject.Components.Cast
{
    public class CircleCastComponent : CastComponent 
    {
        [SerializeField] protected float _radius;
        public override void Cast()
        {
            Collider2D hit = Physics2D.OverlapCircle(GetNewPos(), _radius, _layer);
            _action?.Invoke(hit != null ? hit.gameObject : null);
        }

        protected override void OnDrawGizmosSelected()
        {
            Handles.color = ColorStore.GetColor(_color);
            Handles.DrawSolidDisc(GetNewPos(), Vector3.forward, _radius);
        }
    }
}
