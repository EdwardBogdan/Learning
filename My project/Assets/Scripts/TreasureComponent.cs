using MyProject.Utils;
using UnityEngine;

namespace MyProject.Components
{
    public class TreasureComponent : MonoBehaviour, INaming
    {
        [SerializeField] int _value;

        public string NameElement => "Treasure";

        public void TakeTreasure()
        {
            Data.GameSession.CurrentSession._coins += _value;

            Debug.Log($"Монет собрано: {Data.GameSession.CurrentSession._coins}");
        }
    }
}
