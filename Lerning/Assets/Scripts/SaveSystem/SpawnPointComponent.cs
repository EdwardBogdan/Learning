using UnityEngine;

namespace SaveSystem
{
    public class SpawnPointComponent : IDComponent
    {
        [SerializeField] private Transform point;
        public Vector3 SpawnPos => point.position;
    }
}
