using UnityEngine;

namespace SpawnEquipment
{
    public class SpawnInComponent : MonoBehaviour
    {
        [SerializeField] GameObject _prefab;
        public void SpawnObject(GameObject target)
        {
            Instantiate(_prefab, target.transform);
        }
    }
}
