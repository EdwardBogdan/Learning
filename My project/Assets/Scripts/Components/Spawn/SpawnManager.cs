using System;
using UnityEngine;

namespace MyProject.Components.Spawn
{
    public class SpawnManager : MonoBehaviour
    {
        [SerializeField] SpawnData[] _objects;
        public void SpawnTarget(string name)
        {
            foreach (SpawnData par in _objects)
            {
                if (par._name == name)
                {
                    par.spawner.SpawnObject();
                    break;
                }
            }
        }
        [Serializable]
        class SpawnData
        {
            public string _name;
            public SpawnComponent spawner;
        }
    }
}
