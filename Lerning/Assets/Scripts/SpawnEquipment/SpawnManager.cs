using System;
using UnityEngine;

namespace SpawnEquipment
{
    public class SpawnManager : MonoBehaviour
    {
        [SerializeField] SpawnData[] _objects;
        public GameObject SpawnReturnTarget(string name)
        {
            foreach (SpawnData par in _objects)
            {
                if (par._name == name)
                {
                    return par.spawner.SpawnReturnObject();
                }
            }
            Debug.Log($"{name} - not found");
            return null;
        }

        public void SpawnTarget(string name)
        {
            SpawnReturnTarget(name);
        }

        [Serializable]
        private class SpawnData
        {
            public string _name;
            public SpawnComponent spawner;
        }
    }
}
