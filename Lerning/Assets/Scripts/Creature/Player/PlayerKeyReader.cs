using PhysicModuleSystem;
using UnityEngine.InputSystem;
using UnityEngine;
using Inventory;
using CastSystem2D.Components;

namespace PlayerController
{
    public class PlayerKeyReader : MonoBehaviour
    {
        [SerializeField] private PhysicModuleController _physicController;
        [SerializeField] private CastComponent _interactCast;
        [SerializeField] private GameObject _player;

        //private PlayerAnimatorController _animator;

        private void Awake()
        {
            //_actionSet = _player.GetComponent<PlayerActionSet>();
            //_animator = _player.GetComponent<PlayerAnimatorController>();
        }
        public void OnKeyMovement(InputAction.CallbackContext context)
        {
            Vector2 vector = context.ReadValue<Vector2>();
            _physicController.SetDirection(vector);
        }

        public void OnKeyLMB(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                EquipmentData.I.ItemSpecialAbylityInvoke();
            }
        }
        public void OnKeyUseItem(InputAction.CallbackContext context)
        {
            if (context.canceled)
            {
                EquipmentData.I.UseChoosedItem();
            }
        }
        public void OnKeyInteract(InputAction.CallbackContext context)
        {
            if (context.canceled)
            {
                _interactCast.Cast();
            }
        }

        public void OnKeyChooseItem(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                int index = (int)context.ReadValue<float>();

                EquipmentData.I.ChooseItem(index - 1);
            }
        }
    }
}
