using MyProject.Utils;
using UnityEditor;
using UnityEngine;

namespace MyProject.Components
{
    public class CircleCastComponent : CastComponent 
    {
        [SerializeField] protected float _radius;
        public override string NameElement => $"C.Cast" + base.NameElement;
        public override void Cast()
        {
            Collider2D hit = Physics2D.OverlapCircle(transform.position, _radius, _layer);
            _action?.Invoke(hit != null ? hit.gameObject : null);
        }

        protected override void OnDrawGizmosSelected()
        {
            Handles.color = ColorStore.GetColor(_color);
            Handles.DrawSolidDisc(transform.position, Vector3.forward, _radius);
        }
    }
}
