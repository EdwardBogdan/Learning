using UnityEngine;
using UnityEngine.Events;

namespace MyProject.Components
{
    public class SpawnComponent : MonoBehaviour
    {
        [SerializeField] Transform _target;
        [SerializeField] GameObject _prefab;

        
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
