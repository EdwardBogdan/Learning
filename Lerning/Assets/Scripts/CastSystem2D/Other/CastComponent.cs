using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CastSystem2D.Components
{
    public abstract class CastComponent : CastCore
    {
        [CustomColorPack.Attributes.ColorChoose][SerializeField]
        protected Color _color;

        [CustomUnityUtils.Attributes.Tag][SerializeField]
        protected string _tag;

        [SerializeField] internal bool _useByCombinator;
        [SerializeField] protected bool _tagFilter;

        [SerializeField] protected Vector2 _position;

        [SerializeField] protected LayerMask _layer;

        [ContextMenu("Cast")]
        public void Cast()
        {
            if (_action == null) return;

            InvokeAction(Collect());
        }
        protected Vector3 GetNewPos()
        {
            Vector3 currentPos = transform.position;
            Vector3 scale = transform.lossyScale;
            return new((currentPos.x + _position.x) * scale.x, (currentPos.y + _position.y) * scale.y, 0f);
        }
        protected List<Collider2D> SortToList(Collider2D[] mas)
        {
            List<Collider2D> list = new();

            IEnumerable<Collider2D> filteredColliders = mas;

            if (_tagFilter)
            {
                filteredColliders = filteredColliders.Where(item => item.gameObject.CompareTag(_tag));

                list = filteredColliders.ToList();
            }
            else
            {
                foreach (var item in mas)
                {
                    list.Add(item);
                }
            }

            return list;
        }
        public abstract List<Collider2D> Collect();

#if UNITY_EDITOR
        protected abstract void OnDrawGizmosSelected();
#endif
    }


    public abstract class CastCore : MonoBehaviour
    {
        [SerializeField] protected bool _actionByNull;
        [SerializeField] protected bool _onlyFirstCollected;

        [SerializeField] protected CustomUnityUtils.UnityEvents.UnityEvent_GameObject _action;

        protected void InvokeAction(List<Collider2D> list)
        {
            int count = list.Count;

            if (_actionByNull && count == 0)
            {
                _action?.Invoke(null);
                return;
            }

            if (_onlyFirstCollected && count > 0)
            {
                _action.Invoke(list[0].gameObject);
                return;
            }

            foreach (Collider2D hit in list)
            {
                _action.Invoke(hit.gameObject);
            }
        }
    }
}

