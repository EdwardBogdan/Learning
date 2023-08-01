using UnityEngine;

namespace MyProject.Components
{
    public class HealDamageComponent : MonoBehaviour
    {
        [SerializeField] int _value;

        public void MakeDamage(GameObject _object)
        {
            HealthComponent healthComponent = _object.GetComponent<HealthComponent>();
            healthComponent?.ApplyDamage(_value);
        }
        public void MakeHeal(GameObject _object)
        {
            HealthComponent healthComponent = _object.GetComponent<HealthComponent>();
            healthComponent?.ApplyHeal(_value);
        }
    }
}
