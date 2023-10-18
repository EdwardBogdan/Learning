using UnityEngine;
using UnityEngine.Events;
using MyProject.Utils;

namespace MyProject.Components.Health
{
    public class HealthComponent : MonoBehaviour
    {
        [SerializeField] bool _immortal;
        [SerializeField] int _health;
        [SerializeField] int _maxHealth;
        [SerializeField] float _hitVelocity;
        [SerializeField] Cooldown _immunity;

        [SerializeField] UnityEvent _onHit;
        [SerializeField] UnityEvent _onHeal;
        [SerializeField] UnityEvent _onDead;
        [SerializeField] UnityEvent_Int _onChange;

        public Cooldown ImmunityCooldown => _immunity;
        public int Health => _health;
        public int MaxHealth => _maxHealth;
        public float HitVelocity => _hitVelocity;

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
            if (_immortal) damage = 0;
            _health -= damage;

            _onChange?.Invoke(_health);

            if (_health <= 0)
            {
                _onDead?.Invoke();
                return;
            }
            _onHit?.Invoke();
        }
    }
}
