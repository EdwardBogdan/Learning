using Inventory.Attribute;
using PlayerController;
using UnityEngine;

namespace SpawnEquipment
{
    public class SpawnItemMessageComponent : MonoBehaviour
    {
        [Items, SerializeField] private string _item;
        [SerializeField] private Vector2 _offset;
        [SerializeField] private GameObject _succesfullMessage;
        [SerializeField] private GameObject _unsuccesfullMessage;
        [SerializeField] private Sprite _icon;

        public void SpawnMessage(bool value)
        {
            GameObject obj;
            if (value) obj = Instantiate(_succesfullMessage);
            else obj = Instantiate(_unsuccesfullMessage);
            
            if (_icon != null && obj.TryGetComponent(out SpriteRenderer component))
            {
                component.sprite = _icon;
            }
            Vector3 newOffset = new(_offset.x, _offset.y, 0f);
            obj.transform.position = newOffset + transform.position;
        }
        [ContextMenu("Set icon from prefab item")]
        private void SetIcon()
        {
           _icon =  PlayerCore.ItemDefs.GetItemSprite(_item);
        }
    }
}
