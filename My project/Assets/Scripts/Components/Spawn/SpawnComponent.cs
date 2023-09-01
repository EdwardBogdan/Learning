using MyProject.Utils;
using UnityEngine;

namespace MyProject.Components
{
    public class SpawnComponent : MonoBehaviour , INaming
    {
        [SerializeField] Transform _target;
        [SerializeField] GameObject _prefab;

        public string NameElement => $"Spawn ({_prefabName})";
        private string _prefabName => _prefab ? _prefab.name : null;
        public GameObject SpawnObject()
        {
            GameObject prefab = Instantiate(_prefab, _target.position, Quaternion.identity);
            prefab.transform.localScale = transform.lossyScale;
            return prefab;
        }
        [ContextMenu("Spawn")]
        public void Spawn()
        {
            SpawnObject();
        }
    }
}
