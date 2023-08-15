using UnityEngine;

namespace MyProject.Components
{
    public class HealMakerComponent : MonoBehaviour
    {
        [SerializeField] int _value;

        public void MakeHeal(GameObject _object)
        {
            HealthComponent healthComponent = _object.GetComponent<HealthComponent>();
            healthComponent?.ApplyHeal(_value);
        }
    }
}
