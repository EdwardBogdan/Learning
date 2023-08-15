using MyProject.Components;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MyProject.Player
{
    public class PlayerInputReader : MonoBehaviour
    {
        [SerializeField] PlayerController player;

        [Space(10)]
        [Header("Wallhit Checker")]
        [SerializeField] string[] _checkTags;
        [SerializeField] Vector3 _leftWallCheckerStart;
        [SerializeField] Vector3 _leftWallCheckerFinish;
        [Space(10)]
        [SerializeField] Vector3 _rightWallCheckerStart;
        [SerializeField] Vector3 _rightWallCheckerFinish;

        PlayerPhysicController _playerPhysic;
        Vector3 _currentPosition => transform.position;

        private void Awake()
        {
            _playerPhysic = GetComponent<PlayerPhysicController>();
        }
        void Update()
        {
            _playerPhysic.SetFlags(IsWallHitLeft(), IsWallHitRight());
        }
        public void OnMovement(InputAction.CallbackContext context)
        {
            Vector2 vector = context.ReadValue<Vector2>();
            _playerPhysic.SetDirection(vector);
        }
        public void OnAttackSimple(InputAction.CallbackContext context)
        {
            if (context.canceled)
            {
                player.AttackSimpleTriger();
            }
        }
        public void OnInteract(InputAction.CallbackContext context)
        {
            if (context.canceled)
            {
                player.InteractTriger();
            }
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
        void OnDrawGizmosSelected()
        {
            Gizmos.color = IsWallHitLeft() ? Color.red : Color.green;
            Gizmos.DrawLine(_currentPosition + _leftWallCheckerStart, _currentPosition + _leftWallCheckerFinish);

            Gizmos.color = IsWallHitRight() ? Color.red : Color.green;
            Gizmos.DrawLine(_currentPosition + _rightWallCheckerStart, _currentPosition + _rightWallCheckerFinish);
        }
    }

}
