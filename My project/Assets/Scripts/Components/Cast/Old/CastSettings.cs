using System;
using UnityEngine;

namespace MyProject.Components.Settings
{
    [Serializable]
    class LayerSettings
    {
        public string _name;
        [SerializeField] internal bool _cast;
        public bool _showGizmos;
        public bool _touchAction;
        public bool _oneTime;
        public LayerMask _checkLayer;
        public UnityEvent_GameObject _action;

        [HideInInspector] public GameObject _touchedObject;

        public void SetCast(bool _value)
        {
            _cast = _value;
            _showGizmos = _value;
        }
    }
    [Serializable]
    class BoxCastSettings
    {
        public string _name;
        public Vector2 _CheckPoint;
        public Vector2 _CheckSize;
        public LayerSettings[] _layers;
    }
    [Serializable]
    class CircleCastSettings
    {
        public string _name;
        public Vector2 _CheckPoint;
        public float _radius;
        public LayerSettings[] _layers;
    }
}
