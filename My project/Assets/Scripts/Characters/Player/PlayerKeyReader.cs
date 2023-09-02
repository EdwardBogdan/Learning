using UnityEngine;
using UnityEngine.InputSystem;

namespace MyProject.Characters.Player
{
    public class PlayerKeyReader : MonoBehaviour
    {
        PlayerBehaviour _behaviourTriggers;
        PlayerPhysicController _playerPhysic;

        private void Awake()
        {
            _playerPhysic = GetComponent<PlayerPhysicController>();
            _behaviourTriggers = GetComponent<PlayerBehaviour>();
        }
        public void OnKeyMovement(InputAction.CallbackContext context)
        {
            Vector2 vector = context.ReadValue<Vector2>();
            _playerPhysic.SetDirection(vector);
        }
        public void OnKeyAttack(InputAction.CallbackContext context)
        {
            if (context.canceled)
            {
                _behaviourTriggers.OnTriggerAttack();
            }
        }
        public void OnKeyThrow(InputAction.CallbackContext context)
        {
            if (context.canceled)
            {
                _behaviourTriggers.OnTriggerSpecialAttack();
            }
        }
        public void OnKeyInteract(InputAction.CallbackContext context)
        {
            if (context.canceled)
            {
                _behaviourTriggers.OnTriggerInteract();
            }
        }
    }

}
