using MyProject.Attributes;
using MyProject.Data;
using MyProject.Utils;
using UnityEngine;
using UnityEngine.Events;

namespace MyProject.Components
{
    public class InventoryRequireComponent : MonoBehaviour
    {
        [InventoryId][SerializeField] string _id;
        [SerializeField] int _value;
        [SerializeField] bool _removeAfterUse;

        [SerializeField] private UnityEvent _onSucces;
        [SerializeField] private UnityEvent _onFail;


        public void CheckItem()
        {
            GameSession session = GameSession.CurrentSession;

            int currentCount = session.Inventory.GetCount(_id);

            if (currentCount >= _value)
            {
                if (_removeAfterUse)
                {
                    session.Inventory.Remove(_id, _value);
                }

                _onSucces?.Invoke();
            }
            else
            {
                Debug.Log($"Sorry, but you need {_id}");
                _onFail?.Invoke();
            }
        }
    }
}
