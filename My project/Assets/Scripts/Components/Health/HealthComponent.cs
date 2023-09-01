using UnityEngine;
using UnityEngine.Events;
using System.Collections;

namespace MyProject.Components
{
    public class HealthComponent : MonoBehaviour
    {
        [SerializeField] bool _immortal;
        [SerializeField] int _health;
        [SerializeField] int _maxHealth;
        [SerializeField] float _hitVelocity;
        [SerializeField] float _immunityTime;
        [SerializeField] UnityEvent _onHit;
        [SerializeField] UnityEvent _onHeal;
        [SerializeField] UnityEvent _onDead;
        [SerializeField] UnityEvent_Int _onChange;

        public int Health => _health;
        public int MaxHealth => _maxHealth;

        bool _immunityActivated = false;
        public void SetHealth(int value)
        {
            _health = Mathf.Clamp(value, 0, _maxHealth);
        }
        public void SetMaxHealth(int value)
        {
            _maxHealth = value;
        }
        public void ApplyHeal(int value)
        {
            _health += value;

            _onChange?.Invoke(_health);

            if (_health > _maxHealth)
            {
                _health = _maxHealth;
            }
        }
        public void ApplyDamage(int damage)
        {
            if (!_immunityActivated)
            {
                if (_immortal) damage = 0;
                _health -= damage;

                _onChange?.Invoke(_health);

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
