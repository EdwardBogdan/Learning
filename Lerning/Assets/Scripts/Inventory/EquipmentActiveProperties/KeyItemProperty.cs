using CustomUnityEquipment;
using CustomUnityUtils.Attributes;
using PlayerController;
using UnityEngine;

namespace Inventory.ItemProperty
{
    [CreateAssetMenu(menuName = "Data/Inventory/ItemPropertys/Key Item Property", fileName = "Key Item Property")]
    internal class KeyItemProperty : EquipmentActiveProperty
    {
        [SerializeField] private float _radius;
        [Tag][SerializeField] private string _tag;
        [SerializeField] private LayerMask _lockLayer;
        internal override void ActiveProperty()
        {
            var pos = PlayerCore.Player.transform.position;

            Collider2D[] hits = Physics2D.OverlapCircleAll(pos, _radius, _lockLayer);

            foreach (var hit in hits)
            {
                if (!hit.gameObject.CompareTag(_tag)) continue;

                if (!hit.gameObject.TryGetComponent(out ActionMasComponent component))
                {
                    Debug.Log($"Broken Lock: {hit.gameObject.name}");
                    return;
                };

                bool isPossible = EquipmentData.I.ModifyItem(_equipmentId, _removeFromUse, false);

                if (isPossible)
                {
                    component.InvokeActionMas();
                }

            }
        }
    }
}
