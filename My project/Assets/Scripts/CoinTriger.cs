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
            CoinCounter.AddCoin(_value);
            Destroy(gameObject);
        }
    }

}
