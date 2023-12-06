using UnityEngine;

namespace PhysicComponents
{
    public class ProjectileBase : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private bool _invertX;

        private Rigidbody2D _rb;
        private int _direction;

        private void Start()
        {
            var mod = _invertX ? -1 : 1;
            _direction = mod * transform.localScale.x > 0 ? 1 : -1;
            _rb = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            var position = _rb.position;
            position.x += _direction * _speed;
            _rb.MovePosition(position);
        }
    }
}
