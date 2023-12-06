using PlayerController;
using CustomUnityUtils.TimeUtils;
using UnityEngine;
using UnityEngine.Events;
using AnimatorController;

namespace Inventory.ItemProperty
{
    [CreateAssetMenu(menuName = "Data/Inventory/ItemPropertys/Sword Item Property", fileName = "SwordItemProperty")]
    internal class SwordItemProperty : EquipmentActiveProperty
    {
        [SerializeField] private string _keyArmed;
        [SerializeField] private string _keyDisarmed;
        [SerializeField] private string _keyAttackEvent;
        [SerializeField] private string _keyThrowEvent;
        [SerializeField] private Cooldown cdThrow;
        [SerializeField] private Cooldown cdAttack;

        private UnityEvent _attackEvent;
        private UnityEvent _throwEvent;

        internal override void OnSelected()
        {
            EquipmentData.I.OnRemoved +=OnRemoved;
            EquipmentData.I.ItemSpecicalAbylity += Attack;

            CreatureAnimatorController controller = PlayerCore.PlayerAnimator;

            controller.InvokeEvent(_keyArmed);

            _attackEvent = controller.GetEvent(_keyAttackEvent);
            _throwEvent = controller.GetEvent(_keyThrowEvent);

            cdThrow.Ready();
            cdAttack.Ready();
        }

        internal override void ActiveProperty()
        {
            if (cdThrow.IsReady)
            {
                bool isPossible = EquipmentData.I.ModifyItem(_equipmentId, _removeFromUse, false);

                if (isPossible)
                {
                    cdThrow.Reset();
                    _throwEvent.Invoke();
                }
            }
        }

        private void OnRemoved(EquipmentClass item)
        {
            PlayerCore.PlayerAnimator.InvokeEvent(_keyDisarmed);
            EquipmentData.I.OnRemoved -= OnRemoved;
            EquipmentData.I.ItemSpecicalAbylity -= Attack;
        }
        private void Attack()
        {
            if (cdAttack.IsReady)
            {
                cdAttack.Reset();
                _attackEvent.Invoke();
            }  
        }
    }
}
