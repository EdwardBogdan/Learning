using MyProject.Utils;
using UnityEngine;
using MyProject.Attributes;
using MyProject.Data;

namespace MyProject.Components
{
    public class InventoryAddComponent : MonoBehaviour, INaming
    {
        [InventoryId][SerializeField] string _id;
        [SerializeField] int _value;

        public string NameElement => "Item Add";

        public void TakeTreasure()
        {
            InventoryData inventory = GameSession.CurrentSession.Inventory;

            int count = inventory.GetCount(_id);
            int maxCount = inventory.GetMaxCount(_id);

            if (maxCount + 1  > count)
            {
                inventory.Add(_id, _value);
            }
        }
    }
}
