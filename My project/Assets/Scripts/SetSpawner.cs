using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyProject.Player;

namespace MyProject.Components.Spawner
{
    public class SetSpawner : MonoBehaviour
    {
        [SerializeField] bool _LRSpawn;
        public void SetSpawn()
        {
            PlayerSpawner._LRPosition = _LRSpawn;
        }
    }
}
