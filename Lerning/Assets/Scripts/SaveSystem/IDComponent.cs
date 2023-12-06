using UnityEngine;

namespace SaveSystem
{
    public class IDComponent : MonoBehaviour
    {
        [SerializeField] protected string _id;

        public string ID => _id;

        #region Editor
#if UNITY_EDITOR

        [ContextMenu("CreateID")]
        private void CreateID()
        {
            ClearID();
            _id = StoreID.I.CreateID();
        }

        [ContextMenu("ClearID")]
        private void ClearID()
        {
            if (StoreID.I.ExistID(_id))
            {
                StoreID.I.Remove(_id);
                _id = "";
            }
        }
#endif
        #endregion
    }
}
