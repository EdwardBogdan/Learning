using System;
using UnityEngine;

namespace MyProject.Components
{
    public class DamageMakerComponent : MonoBehaviour
    {
        [SerializeField] int _damage;

        public void DealDamage(GameObject _object)
        {
            HealthComponent healthComponent = _object?.GetComponent<HealthComponent>();
            healthComponent?.ApplyDamage(_damage);
        }
    }
}
