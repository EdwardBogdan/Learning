using MyProject.Utils;
using UnityEngine;

namespace MyProject.Components.Cast
{
    public abstract class CastComponent : MonoBehaviour, INaming
    {
        [SerializeField] protected Vector2 _position;
        [SerializeField] protected TypeColor _color;
        [SerializeField] protected LayerMask _layer;
        [SerializeField] protected UnityEvent_GameObject _action;

        public virtual string NameElement => LayersName();
        public abstract void Cast();
        protected abstract void OnDrawGizmosSelected();
        protected string LayersName()
        {
            string combinedLayerNames = "(";
            bool firstLayerAdded = false;

            for (int i = 0; i < 32; i++)
            {
                if ((_layer.value & (1 << i)) != 0)
                {
                    if (firstLayerAdded)
                    {
                        combinedLayerNames += ", ";
                    }
                    combinedLayerNames += LayerMask.LayerToName(i);
                    firstLayerAdded = true;
                }
            }
            combinedLayerNames += ")";
            return combinedLayerNames;
        }
        protected Vector3 GetNewPos()
        {
            Vector3 currentPos = transform.position;
            Vector3 scale = transform.lossyScale;
            return new((currentPos.x + _position.x), (currentPos.y + _position.y), 0f);
        }
    }
}

