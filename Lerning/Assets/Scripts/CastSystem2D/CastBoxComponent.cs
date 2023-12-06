using System.Collections.Generic;
using UnityEngine;

namespace CastSystem2D.Components
{
    public class CastBoxComponent : CastComponent
    {
        [SerializeField] protected Vector2 _checkArea;
        [Range(0,360)]public int _rotationAngle;

        public override List<Collider2D> Collect()
        {
            Collider2D[] hits = Physics2D.OverlapBoxAll(GetNewPos(), _checkArea, _rotationAngle, _layer);

            return SortToList(hits);
        }
#if UNITY_EDITOR
        protected override void OnDrawGizmosSelected()
        {
            Gizmos.color = _color;

            Matrix4x4 rotationMatrix = Matrix4x4.TRS(GetNewPos(), Quaternion.Euler(0, 0, _rotationAngle), Vector3.one);

            Gizmos.matrix = rotationMatrix;

            Gizmos.DrawCube(Vector3.zero, _checkArea);
        }
#endif
    }
}
