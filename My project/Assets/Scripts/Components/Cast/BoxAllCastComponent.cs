using UnityEngine;

namespace MyProject.Components.Cast
{
    public class BoxAllCastComponent : BoxCastComponent
    {
        [SerializeField] private bool _nullAction;
        public override void Cast()
        {
            Collider2D[] hits = Physics2D.OverlapBoxAll(GetNewPos(), _CheckSize, 0f, _layer);
            if (hits.Length > 0 && _nullAction)
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
