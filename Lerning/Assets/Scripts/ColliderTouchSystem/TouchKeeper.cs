using CustomUnityUtils.UnityEvents;
using System.Collections.Generic;
using UnityEngine;

namespace ColliderTouchSystem
{
    public class TouchKeeper : MonoBehaviour
    {
        [SerializeField] private TouchComponent _touchComponent;

        [SerializeField] UnityEvent_GameObject _OnEnter;
        [SerializeField] UnityEvent_GameObject _OnExit;

        [Tooltip("The list only for checking, don`t make changes in")]
        [SerializeField] private List<GameObject> _inColliderList = new();

        public void AddTouch(GameObject _object)
        {
            if (_inColliderList.Count <= 0)
            {
                _OnEnter.Invoke(_object);
            }
            _inColliderList.Add(_object);
            _touchComponent.ChangeTouch(_object);
        }
        public void RemoveTouch(GameObject _object)
        {
            _inColliderList.Remove(_object);

            if (_inColliderList.Count <= 0)
            {
                _touchComponent.ChangeTouch(null);
                _OnExit.Invoke(_object);
            }
        }
    }
}
