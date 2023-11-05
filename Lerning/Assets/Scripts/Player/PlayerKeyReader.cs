using PhysicModuleSystem;
using UnityEngine.InputSystem;
using UnityEngine;
using Core;
using SoundsAndUI.UIElements;

namespace PlayerController
{
    public class PlayerKeyReader : MonoBehaviour
    {
        [SerializeField] private PhysicModuleController _physicController;
        [SerializeField] private GameObject _player;

        //private PlayerActionSet _actionSet;
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

        //public void OnKeyAttack(InputAction.CallbackContext context)
        //{
        //    if (context.canceled)
        //    {
        //        _animator.ACTriggerAttack();
        //    }
        //}
        public void OnKeyUseItem(InputAction.CallbackContext context)
        {
            if (context.canceled)
            {
                PlayerCore.PlayerEquipmentFunctional.UseChoosedItem();
            }
        }
        //public void OnKeyInteract(InputAction.CallbackContext context)
        //{
        //    if (context.canceled)
        //    {
        //        _actionSet.OnTriggerInteract();
        //    }
        //}
        //public void OnKeyHeal(InputAction.CallbackContext context)
        //{
        //    if (context.canceled)
        //    {
        //        _actionSet.OnUseHeal();
        //    }
        //}

        public void OnKeyChooseItem(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                int index = (int)context.ReadValue<float>();

                PlayerCore.PlayerEquipmentFunctional.ChooseItem(index);
                HudEquipments.Hud.ChooseItem(index);
            }
        }
    }
}
