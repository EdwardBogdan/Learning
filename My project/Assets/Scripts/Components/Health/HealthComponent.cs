using UnityEngine;
using UnityEngine.Events;
using System.Collections;

namespace MyProject.Components
{
    public class HealthComponent : MonoBehaviour
    {
        [SerializeField] int _health;
        [SerializeField] int _maxHealth;
        [SerializeField] float _immunityTime;
        [SerializeField] UnityEvent _onHit;
        [SerializeField] UnityEvent _onHeal;
        [SerializeField] UnityEvent _onDead;

        public int Health => _health;
        public int MaxHealth => _maxHealth;

        bool _immunityActivated = false;
        public void ApplyHeal(int value)
        {
            _health += value;
            if (_health > _maxHealth)
            {
                _health = _maxHealth;
            }
        }
        public void ApplyDamage(int damage)
        {
            if (!_immunityActivated)
            {
                _health -= damage;
                if (_health <= 0)
                {
                    _onDead?.Invoke();
                    return;
                }
                StartCoroutine(ImmunityActivator());
                _onHit?.Invoke();
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
