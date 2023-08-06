using UnityEngine;
using UnityEngine.Events;

namespace MyProject.Components
{
    public class SpawnComponent : MonoBehaviour
    {
        [SerializeField] Transform _target;
        [SerializeField] GameObject _prefab;

        [ContextMenu("Spawn")]
        public GameObject Spawn()
        {
            GameObject prefab = Instantiate(_prefab, _target.position, Quaternion.identity);
            prefab.transform.localScale = transform.lossyScale;
            return prefab;
        }
    }
}
