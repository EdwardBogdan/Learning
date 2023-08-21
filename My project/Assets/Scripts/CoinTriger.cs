using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyProject.Components
{
    public class CoinTriger : MonoBehaviour
    {
        [SerializeField] int _value;

        public void TakeCoin()
        {
            Data.GameSession.CurrentSession._coins += _value;

            Debug.Log($"Монет собрано: {Data.GameSession.CurrentSession._coins}");
        }
    }
}
