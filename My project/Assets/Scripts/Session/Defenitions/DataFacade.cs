using UnityEngine;

namespace MyProject.Data.Defenitions
{
    [CreateAssetMenu(menuName = "Data/DataFacade", fileName = "DataFacade")]
    public class DataFacade : ScriptableObject
    {
        [SerializeField] private InventoryItemsDef _itemsRef;
        [SerializeField] private SaveData saveDataRef;


        public InventoryItemsDef ItemDefs => _itemsRef;
        public SaveData LastSave => saveDataRef;



        private static DataFacade _instance;
        public static DataFacade I => _instance == null ? LoadItemDefs() : _instance;

        private static DataFacade LoadItemDefs()
        {
            return _instance = Resources.Load<DataFacade>("DataFacade");
        }
    }
}
