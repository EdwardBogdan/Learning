using Core.Data.ObservableProperty;
using UnityEngine;

namespace Core.Characters
{
    [CreateAssetMenu(menuName = "Data/Player Characterictics/Player Current Characterictics", fileName = "PlayerCurrentCharacterictics")]
    public class PlayerCharacteristics : ScriptableObject
    {
        [SerializeField][Range(0,50)] private int _minOfMaxHealth = 30;
        [SerializeField][Range(50, 150)] private int _maxOfMaxHealth = 90;

        [SerializeField] private IntProperty _health = new();
        [SerializeField] private IntProperty _maxHealth = new();


        public IntProperty Health => _health;
        public IntProperty MaxHealth => _maxHealth;


        private static PlayerCharacteristics _instance;
        internal static PlayerCharacteristics I => _instance == null ? LoadSingleton() : _instance;


        private static PlayerCharacteristics LoadSingleton()
        {
            return _instance = Resources.Load<PlayerCharacteristics>("PlayerData/PlayerCurrentCharacterictics");
        }



        private void OnValidate()
        {
            bool isMaxHealthOutMaxLimit = _maxHealth.Value > _maxOfMaxHealth;
            bool isMaxHealthOutMinLimit = _maxHealth.Value < _minOfMaxHealth;

            if (isMaxHealthOutMaxLimit)
            {
                _maxHealth.Value = _maxOfMaxHealth;
            }
            else if (isMaxHealthOutMinLimit)
            {
                _maxHealth.Value = _minOfMaxHealth;
            }
            else
            {
                _maxHealth.Validate();
            }

            bool isHealthOutMaxLimit = _health.Value > _maxHealth.Value;
            bool isHealthOutMinLimit = _health.Value < 0;

            if (isHealthOutMaxLimit)
            {
                _health.Value = _maxHealth.Value;
            }
            else if (isHealthOutMinLimit)
            {
                _health.Value = 0;
            }
            else
            {
                _health.Validate();
            }
        }
    }
}
