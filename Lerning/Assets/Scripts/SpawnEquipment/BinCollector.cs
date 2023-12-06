using UnityEngine;

namespace SpawnEquipment
{
    public class BinCollector : MonoBehaviour
    {
        [SerializeField] private string _binName;
        [SerializeField] private GameObject _object;

        private void Awake()
        {
            if (_binName == "") return;

            GameObject bin = GameObject.Find(_binName);

            if (bin == null)
            {
                bin = new GameObject(_binName);
            }

            _object.transform.SetParent(bin.transform);

            Destroy(this);
        }
    }
}