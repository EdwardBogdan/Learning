using MyProject.Physic.Modules;
using MyProject.Physic.PAController;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MyProject.Characters.Player
{
    public class PlayerKeyReader : MonoBehaviour
    {
        [SerializeField] private GameObject _player;

        private PlayerActionSet _actionSet;
        private PhysicModuleController _controller;
        private PlayerAnimatorController _animator;

        private void Awake()
        {
            _actionSet = _player.GetComponent<PlayerActionSet>();
            _controller = _player.GetComponent<PhysicModuleController>();
            _animator = _player.GetComponent<PlayerAnimatorController>();
        }
        public void OnKeyMovement(InputAction.CallbackContext context)
        {
            Vector2 vector = context.ReadValue<Vector2>();
            _controller.SetDirection(vector);
        }
        public void OnKeyAttack(InputAction.CallbackContext context)
        {
            if (context.canceled)
            {
                _animator.ACTriggerAttack();
            }
        }
        public void OnKeyThrow(InputAction.CallbackContext context)
        {
            if (context.canceled)
            {
                _animator.ACTriggerThrow();
            }
        }
        public void OnKeyInteract(InputAction.CallbackContext context)
        {
            if (context.canceled)
            {
                _actionSet.OnTriggerInteract();
            }
        }
    }

}
