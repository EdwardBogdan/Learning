using UnityEngine;
using UnityEngine.InputSystem;

namespace Project.Player
{
    public class PlayerInputReader : MonoBehaviour
    {
        [SerializeField] PlayerControler player;
        [SerializeField] Vector3 _groundCheckPosition;
        [SerializeField] float _groundCheckRadius;
        [SerializeField] LayerMask _groundCheckLayer;

        void FixedUpdate()
        {
            player.isGrounded = isGrounded();
        }
        public void OnMovement(InputAction.CallbackContext context)
        {
            Vector2 vector = context.ReadValue<Vector2>();
            player.SetDirection(vector);
        }
        public void OnSayHello(InputAction.CallbackContext context)
        {
            if (context.canceled)
            {
                Debug.Log("Hello World");
            }
        }
        bool isGrounded()
        {
            var hit = Physics2D.CircleCast(transform.position + _groundCheckPosition, _groundCheckRadius, Vector3.down, 0, _groundCheckLayer);
            return hit.collider != null;
        }
        void OnDrawGizmos()
        {
            Gizmos.color = isGrounded() ? Color.green : Color.red;
            Gizmos.DrawSphere(transform.position + _groundCheckPosition, _groundCheckRadius);
        }
    }

}
