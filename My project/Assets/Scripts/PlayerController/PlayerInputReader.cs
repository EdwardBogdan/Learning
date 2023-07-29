using UnityEngine;
using UnityEngine.InputSystem;

namespace MyProject.Player
{
    public class PlayerInputReader : MonoBehaviour
    {
        [SerializeField] PlayerControler player;

        [Space(10)]
        [Header("Ground Checker")]
        [SerializeField] Vector3 _groundCheckPosition;
        [SerializeField] float _groundCheckRadius;
        [SerializeField] LayerMask _groundCheckLayer;

        [Space(10)]
        [Header("Wallhit Checker")]
        [SerializeField] Vector3 _leftWallCheckerStart;
        [SerializeField] Vector3 _leftWallCheckerFinish;
        [Space(10)]
        [SerializeField] Vector3 _rightWallCheckerStart;
        [SerializeField] Vector3 _rightWallCheckerFinish;

        Vector3 _currentPosition;

        void FixedUpdate()
        {
            _currentPosition = transform.position;
            player.isGrounded = isGrounded();
            player.isHitWallLeft = isWallHitLeft();
            player.isHitWallRight = isWallHitRight();
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
            var hit = Physics2D.CircleCast(_currentPosition + _groundCheckPosition, _groundCheckRadius, Vector3.down, 0, _groundCheckLayer);
            return hit.collider != null;
        }
        bool isWallHitLeft()
        { 
            RaycastHit2D hit = Physics2D.Linecast(_currentPosition + _leftWallCheckerStart, _currentPosition + _leftWallCheckerFinish);

            if (hit.collider != null)
            {
                GameObject obj = hit.collider.gameObject;
                if (!obj.CompareTag("Player") && !obj.CompareTag("Platform"))
                {
                    return true;
                }
            }
            return false;
        }
        bool isWallHitRight()
        {
            var hit = Physics2D.Linecast(_currentPosition + _rightWallCheckerStart, _currentPosition + _rightWallCheckerFinish);

            if (hit.collider != null)
            {
                GameObject obj = hit.collider.gameObject;
                if (!obj.CompareTag("Player") && !obj.CompareTag("Platform"))
                {
                    return true;
                }
            }
            return false;
        }
        void OnDrawGizmos()
        {
            Gizmos.color = isGrounded() ? Color.green : Color.red;
            Gizmos.DrawSphere(_currentPosition + _groundCheckPosition, _groundCheckRadius);

            Gizmos.color = isWallHitLeft() ? Color.red : Color.green;
            Gizmos.DrawLine(_currentPosition + _leftWallCheckerStart, _currentPosition + _leftWallCheckerFinish);

            Gizmos.color = isWallHitRight() ? Color.red : Color.green;
            Gizmos.DrawLine(_currentPosition + _rightWallCheckerStart, _currentPosition + _rightWallCheckerFinish);
        }
    }

}
