using MyProject.Utils;
using UnityEngine;

namespace MyProject.Components
{
    public class HealMakerComponent : MonoBehaviour, INaming
    {
        [SerializeField] int _value;

        public string NameElement => "Heal";

        public void MakeHeal(GameObject _object)
        {
            HealthComponent healthComponent = _object.GetComponent<HealthComponent>();
            healthComponent?.ApplyHeal(_value);
        }
    }
}
