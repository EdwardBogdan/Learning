using UnityEngine;

namespace MyProject.Components.Object
{
    public class Spike : MonoBehaviour
    {
        DamageMakerComponent _damageComponent;
        private void Awake()
        {
            _damageComponent = GetComponent<DamageMakerComponent>();
        }
    }
}

