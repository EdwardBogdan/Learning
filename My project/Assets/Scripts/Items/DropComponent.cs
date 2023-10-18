using System;
using System.Reflection;
using System.Xml.Linq;
using UnityEngine;

namespace MyProject.Components
{
    public class DropComponent : MonoBehaviour
    {
        [SerializeField] Drop[] _dropList;

        [SerializeField] DropProcessor _droper;

        [ContextMenu("Drop")]
        public void Drop()
        {
            GameObject _object = Instantiate(_droper.gameObject);
            _object.transform.position = transform.position;
            DropProcessor _processor = _object.GetComponent<DropProcessor>();
            _processor.Droping(_dropList);
        }
        public void ChangeDropGameObject(int _index, GameObject _object)
        {
            if (CheckArray(_index)) return;

            _dropList[_index]._drop = _object;
        }
        public void ChangeDropCount(int _index, int _value)
        {
            if (CheckArray(_index)) return;

            _dropList[_index]._count = _value;
        }
        public void ChangeDropChance(int _index, int _value)
        {
            if (CheckArray(_index)) return;

            _dropList[_index]._chance = _value;
        }
        public void ChangeDropChanceToEach(int _index, bool _value)
        {
            if (CheckArray(_index)) return;

            _dropList[_index]._chanceToEach = _value;
        }
        private bool CheckArray(int _index)
        {
            return (!(_dropList != null) && !(_index >= 0) && !(_index < _dropList.Length));
        }
    }

    [Serializable]
    public class Drop
    { 
        public GameObject _drop;
        public int _count;
        [Range(0, 100)] public int _chance;
        public bool _chanceToEach;
    }
}

