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
        [SerializeField] string[] _checkTags;
        [SerializeField] Vector3 _leftWallCheckerStart;
        [SerializeField] Vector3 _leftWallCheckerFinish;
        [Space(10)]
        [SerializeField] Vector3 _rightWallCheckerStart;
        [SerializeField] Vector3 _rightWallCheckerFinish;

        Vector3 _currentPosition;

        void Update()
        {
            _currentPosition = transform.position;
            player.SetFlags(IsGrounded(), IsWallHitLeft(), IsWallHitRight());
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
        public void OnInteract(InputAction.CallbackContext context)
        {
            if (context.canceled)
            {
                player.Interact();
            }
        }
        bool IsGrounded()
        {
            var hit = Physics2D.CircleCast(_currentPosition + _groundCheckPosition, _groundCheckRadius, Vector3.down, 0, _groundCheckLayer);
            return hit.collider != null;
        }
        bool IsWallHitLeft()
        {
            // Создаем слой маску, исключая коллайдеры с слоем "CameraBorder"
            int cameraBorderLayer = LayerMask.NameToLayer("CameraBorder");
            LayerMask layerMask = ~(1 << cameraBorderLayer);

            RaycastHit2D hit = Physics2D.Linecast(_currentPosition + _leftWallCheckerStart, _currentPosition + _leftWallCheckerFinish, layerMask);

            if (hit.collider != null)
            {
                GameObject obj = hit.collider.gameObject;
                foreach (string tag in _checkTags)
                {
                    if (obj.CompareTag(tag))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        bool IsWallHitRight()
        {
            // Создаем слой маску, исключая коллайдеры с слоем "CameraBorder"
            int cameraBorderLayer = LayerMask.NameToLayer("CameraBorder");
            LayerMask layerMask = ~(1 << cameraBorderLayer);

            var hit = Physics2D.Linecast(_currentPosition + _rightWallCheckerStart, _currentPosition + _rightWallCheckerFinish, layerMask);

            if (hit.collider != null)
            {
                GameObject obj = hit.collider.gameObject;
                foreach (string tag in _checkTags)
                {
                    if (obj.CompareTag(tag))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        void OnDrawGizmos()
        {
            Gizmos.color = IsGrounded() ? Color.green : Color.red;
            Gizmos.DrawSphere(_currentPosition + _groundCheckPosition, _groundCheckRadius);

            Gizmos.color = IsWallHitLeft() ? Color.red : Color.green;
            Gizmos.DrawLine(_currentPosition + _leftWallCheckerStart, _currentPosition + _leftWallCheckerFinish);

            Gizmos.color = IsWallHitRight() ? Color.red : Color.green;
            Gizmos.DrawLine(_currentPosition + _rightWallCheckerStart, _currentPosition + _rightWallCheckerFinish);
        }
    }

}
