using MyProject.Utils;
using UnityEngine;

namespace MyProject.Components.Cast
{
    public abstract class CastComponent : MonoBehaviour
    {
        [SerializeField] protected Vector2 _position;
        [SerializeField] protected TypeColor _color;
        [SerializeField] protected LayerMask _layer;
        [SerializeField] protected UnityEvent_GameObject _action;

        public abstract void Cast();
        protected abstract void OnDrawGizmosSelected();
        protected Vector3 GetNewPos()
        {
            Vector3 currentPos = transform.position;
            Vector3 scale = transform.lossyScale;
            return new((currentPos.x + _position.x), (currentPos.y + _position.y), 0f);
        }
    }
}

