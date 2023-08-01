using UnityEngine;
using UnityEngine.Events;
using System.Collections;

namespace MyProject.Components
{
    public class HealthComponent : MonoBehaviour
    {
        [SerializeField] int _health;
        [SerializeField] float _immunityTime;
        [SerializeField] UnityEvent _onHit;
        [SerializeField] UnityEvent _onDead;

        public int Health => _health;

        bool _immunityActivated = false;
        public void ApplyHeal(int value)
        {
            _health += value;
        }
        public void ApplyDamage(int damage)
        {
            if (!_immunityActivated)
            {
                _health -= damage;
                StartCoroutine(ImmunityActivator());
                _onHit?.Invoke();
                if (_health <= 0)
                {
                    _onDead?.Invoke();
                }
            }
        }
        private IEnumerator ImmunityActivator()
        {
            _immunityActivated = true;
            yield return new WaitForSeconds(_immunityTime);
            _immunityActivated = false;
        }
    }
}
