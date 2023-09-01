using MyProject.Components;
using UnityEngine;

namespace MyProject.Props
{
    public class Barrel : MonoBehaviour
    {
        [SerializeField] TouchComponent _ground;
        [SerializeField] Rigidbody2D _body;

        private void FixedUpdate()
        {
            if (_body.velocity.y >= 0)
            {
                _ground.Touch(gameObject);
            }
        }
    }
}
