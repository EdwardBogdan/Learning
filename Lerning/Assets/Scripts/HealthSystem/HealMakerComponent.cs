using UnityEngine;

namespace HealthSystem
{
    public class HealMakerComponent : MonoBehaviour
    {
        [SerializeField] int _value;
        public void MakeHeal(GameObject _object)
        {
            if (!_object.TryGetComponent<HealthComponent>(out var healthComponent)) return;
            healthComponent.ApplyHeal(_value);
        }
    }
}
