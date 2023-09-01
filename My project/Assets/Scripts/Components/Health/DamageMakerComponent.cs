using MyProject.Utils;
using UnityEngine;

namespace MyProject.Components
{
    public class DamageMakerComponent : MonoBehaviour, INaming
    {
        [SerializeField] int _damage;

        public string NameElement => "Damage";

        public void DealDamage(GameObject _object)
        {
            HealthComponent healthComponent = _object?.GetComponent<HealthComponent>();
            healthComponent?.ApplyDamage(_damage);

            Vector2 posThis = transform.position;
            Vector2 posObj = _object.transform.position;

            Vector2 direction = posObj - posThis;

            Rigidbody2D rb = _object.GetComponent<Rigidbody2D>();
            rb.velocity = new(0, 0);
            rb.AddForce(direction, ForceMode2D.Impulse);
        }
    }
}
