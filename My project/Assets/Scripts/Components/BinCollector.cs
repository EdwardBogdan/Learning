using UnityEngine;

namespace MyProject.Components.Collector
{
    public class BinCollector : MonoBehaviour
    {
        [SerializeField] private string _binName;
        [SerializeField] private GameObject _object;
        private void Awake()
        {
            if (_binName == "") return;

            GameObject bin = GameObject.Find("#" + _binName);

            if (bin == null) return;

            _object.transform.SetParent(bin.transform);

            Destroy(this);
        }
    }
}