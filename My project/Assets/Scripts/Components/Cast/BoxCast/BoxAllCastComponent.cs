using MyProject.Utils;
using UnityEditor;
using UnityEngine;

namespace MyProject.Components
{
    public class BoxAllCastComponent : BoxCastComponent
    {
        [SerializeField] private bool _nullAction;
        public override string NameElement => $"B.CastAll" + LayersName();
        public override void Cast()
        {
            Collider2D[] hits = Physics2D.OverlapBoxAll(transform.position, _CheckSize, 0f, _layer);
            if (hits == null && _nullAction)
            {
                _action?.Invoke(null);
                return;
            }

            foreach (Collider2D hit in hits)
            {
                _action?.Invoke(hit != null ? hit.gameObject : null);
            }
        }
    }
}
