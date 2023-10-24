using MyProject.Utils;
using UnityEngine;

namespace MyProject.Components.Health
{
    public class DamageMakerComponent : MonoBehaviour
    {
        [SerializeField] int _damage = 1;
        [SerializeField] float _hitVelocityMulti = 1;
        public void DealDamage(GameObject _object)
        {
            if (!_object.TryGetComponent(out HealthComponent healthComponent)) return;

            if (!healthComponent.ImmunityCooldown.IsReady) return;

            healthComponent.ImmunityCooldown.Reset();

            healthComponent.ApplyDamage(_damage);

            Vector2 posThis = transform.position;
            Vector2 posObj = _object.transform.position;

            Vector2 direction = posObj - posThis;
            direction = direction.normalized;

            if (direction.x > 0) direction.x = 1;
            else if (direction.x < 0) direction.x = -1;


            direction = direction * healthComponent.HitVelocity * _hitVelocityMulti;

            Rigidbody2D rb = _object.GetComponent<Rigidbody2D>();
            rb.velocity = new(0, 0);
            rb.AddForce(direction, ForceMode2D.Impulse);
        }
    }
}
