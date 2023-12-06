using UnityEngine;

namespace SpawnEquipment
{
    public class SpawnComponent : MonoBehaviour
    {
        [SerializeField] private bool _spawnInside = false;
        [SerializeField] private bool _useScale = true;
        [SerializeField] private Transform _target;
        [SerializeField] protected GameObject _prefab;

        public virtual GameObject SpawnReturnObject()
        {
            GameObject prefab = Instantiate(_prefab, _target.position, Quaternion.identity);

            Vector3 thisScale = transform.lossyScale;
            Vector3 prefScale = prefab.transform.localScale;

            if (_useScale)
            {
                prefab.transform.localScale = new(thisScale.x * prefScale.x, thisScale.y * prefScale.y, thisScale.z * prefScale.z);
            }

            if (_spawnInside)
            {
                prefab.transform.SetParent(transform);
            }
            return prefab;
        }
        [ContextMenu("Spawn")]
        public void SpawnObject()
        {
            SpawnReturnObject();
        }
    }
}
