using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace CastSystem2D.Components
{
    public class CastCircleComponent : CastComponent 
    {
        [SerializeField] protected float _checkRadius;

        public override List<Collider2D> Collect()
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(GetNewPos(), _checkRadius, _layer);

            return SortToList(hits);
        }

#if UNITY_EDITOR
        protected override void OnDrawGizmosSelected()
        {
            Handles.color = _color;
            Vector2 pos = GetNewPos();
            Handles.DrawSolidDisc(pos, Vector3.forward, _checkRadius);
        }
#endif
    }
}
