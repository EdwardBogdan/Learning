using MyProject.Utils;
using System.Collections.Generic;
using UnityEngine;

namespace MyProject.Components.Triggers
{
    [RequireComponent(typeof(TouchComponent))]
    public class KeeperInColliderComponent : MonoBehaviour
    {
        [SerializeField] UnityEvent_GameObject _OnEnter;
        [SerializeField] UnityEvent_GameObject _OnExit;

        private readonly List<GameObject> _inCollider = new();
        private TouchComponent _touch;

        private void Awake()
        {
            _touch = GetComponent<TouchComponent>();
        }
        public void AddSurface(GameObject _object)
        {
            if (_inCollider.Count <= 0)
            {
                _OnEnter.Invoke(_object);
            }
            _inCollider.Add(_object);
            _touch.Touch(_object);
        }
        public void RemoveSurface(GameObject _object)
        {
            _inCollider.Remove(_object);

            if (_inCollider.Count <= 0)
            {
                _touch.Touch(null);
                _OnExit.Invoke(_object);
            } 
        }
    }
}
