using UnityEngine;

namespace MyProject.Components
{
    public class CircleAllCastComponent : CircleCastComponent
    {
        [SerializeField] private bool _nullAction;
        public override string NameElement => $"C.CastAll" + LayersName();
        public override void Cast()
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, _radius, _layer);
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
