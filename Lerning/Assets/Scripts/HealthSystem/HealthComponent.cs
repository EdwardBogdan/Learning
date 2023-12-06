using UnityEngine;
using UnityEngine.Events;
using CustomUnityUtils.UnityEvents;
using CustomUnityUtils.TimeUtils;

namespace HealthSystem
{
    public class HealthComponent : MonoBehaviour
    {
        [SerializeField] bool _immortal;
        [SerializeField] int _health;
        [SerializeField] int _maxHealth;
        [SerializeField][Range(0,100)] int _hitVelocityImmunity;
        [SerializeField] Cooldown _immunityCd;

        [SerializeField] UnityEvent _onHit;
        [SerializeField] UnityEvent _onHeal;
        [SerializeField] UnityEvent _onDead;
        [SerializeField] UnityEvent_Int _onChange;

        public Cooldown ImmunityCooldown => _immunityCd;
        public int Health => _health;
        public int MaxHealth => _maxHealth;
        public float HitVelocityImmunity => _hitVelocityImmunity;

        public void SetImmortal(bool value)
        {
            _immortal = value;
        }
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

            _onHeal?.Invoke();
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
        
        private void Awake()
        {
            _immunityCd.Ready();
        }
    }
}
