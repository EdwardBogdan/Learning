using MyProject.Utils;
using UnityEngine;

namespace MyProject.Components.Triggers
{
    public class BaseTriggerComponent : MonoBehaviour
    {
        [SerializeField] protected bool _checkByTag;
        [SerializeField] protected string[] _tags;
        [SerializeField] protected bool _checkByLayer;
        [SerializeField] protected LayerMask _layerMask;

        [SerializeField] protected UnityEvent_GameObject _action;

        public virtual string NameElement => "Error";

        protected bool CheckTag(GameObject _object)
        {
            foreach (string tag in _tags)
            {
                if (_object.CompareTag(tag))
                {
                    return true;
                }
            }
            return false;
        }
        protected bool CheckLayer(GameObject _object)
        {
            return (_layerMask.value & (1 << _object.layer)) != 0;
        }
    }
}
