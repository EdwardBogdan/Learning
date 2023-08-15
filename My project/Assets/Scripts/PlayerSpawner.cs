using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyProject.Player
{
    public class PlayerSpawner : MonoBehaviour
    {
        public static bool _LRPosition = true; //true = left, false = right;
        [SerializeField] PlayerController _player;
        [SerializeField] Transform _spawnLeft;
        [SerializeField] Transform _spawnRight;

        private void Awake()
        {
             _player.gameObject.transform.position = _LRPosition ? _spawnLeft.position : _spawnRight.position;
        }
    }
}
