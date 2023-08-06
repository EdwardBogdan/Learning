using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyProject.Components
{
    public class CoinTriger : MonoBehaviour
    {
        [SerializeField] int _value;

        public static int _count = 0;
        public void TakeCoin()
        {
            _count += _value;
            Debug.Log($"Монет собрано: {_count}");
        }
    }
}
