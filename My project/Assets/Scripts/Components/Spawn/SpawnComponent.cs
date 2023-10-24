using MyProject.Utils;
using UnityEngine;

namespace MyProject.Components.Spawn
{
    public class SpawnComponent : MonoBehaviour
    {
        [SerializeField] bool _spawnInside = false;
        [SerializeField] Transform _target;
        [SerializeField] GameObject _prefab;

        public GameObject SpawnObject()
        {
            GameObject prefab = Instantiate(_prefab, _target.position, Quaternion.identity);

            Vector3 thisScale = transform.lossyScale;
            Vector3 prefScale = prefab.transform.localScale;

            prefab.transform.localScale = new(thisScale.x * prefScale.x, thisScale.y * prefScale.y, thisScale.z * prefScale.z);

            if (_spawnInside)
            {
                prefab.transform.SetParent(transform);
            }
            return prefab;
        }
        [ContextMenu("Spawn")]
        public void Spawn()
        {
            SpawnObject();
        }
    }
}
