using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MyProject.Components
{
    public class CoinCounter : MonoBehaviour
    {
        [SerializeField] Text _text;
        static Text sText;
        static int _count = 0;

        private void Awake()
        {
            sText = _text; 
        }
        public static void AddCoin(int value)
        {
            _count += value;
            sText.text = $"{ _count}";
        }
    }
}
