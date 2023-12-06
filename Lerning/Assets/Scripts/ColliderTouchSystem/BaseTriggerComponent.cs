using UnityEngine;
using CustomUnityUtils.UnityEvents;

namespace ColliderTouchSystem
{
    public abstract class BaseTriggerComponent : MonoBehaviour
    {
        [SerializeField] protected bool _checkByTag;
        [SerializeField] protected bool _ignoreByTag;
        [SerializeField] protected string[] _tags;
        [SerializeField] protected string[] _ignoreTags;
        [SerializeField] protected bool _checkByLayer;
        [SerializeField] protected LayerMask _layerMask;

        [SerializeField] protected UnityEvent_GameObject _action;

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
        protected bool TagIgnore(GameObject _object)
        {
            foreach (string tag in _ignoreTags)
            {
                if (!_object.CompareTag(tag))
                {
                    continue;
                }
                return false;
            }
            return true;
        }
        protected bool CheckLayer(GameObject _object)
        {
            return (_layerMask.value & (1 << _object.layer)) != 0;
        }
    }
}
