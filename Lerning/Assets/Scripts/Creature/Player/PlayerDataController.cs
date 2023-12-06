using AnimatorController;
using Core.Characters;
using HealthSystem;
using SaveSystem;
using UnityEngine;

namespace PlayerController
{
    public class PlayerDataController : MonoBehaviour
    {
        [SerializeField] private GameObject _playerGO;
        [SerializeField] private HealthComponent _health;
        [SerializeField] private CreatureAnimatorController _animator;
        private void Start()
        {
            PlayerCore.Player = _playerGO;

            var healthValue = PlayerCharacteristics.I.Health;
            var maxValue = PlayerCharacteristics.I.MaxHealth;
            _health.SetMaxHealth(healthValue.Value);
            _health.SetHealth(maxValue.Value);

            healthValue.OnChanged += OnHealthChange;
            maxValue.OnChanged += OnMaxHealthChange;

            PlayerCore.PlayerAnimator = _animator;

            SpawnPlayer();
        }

        public void OnHealthComponentChange(int value)
        {
            PlayerCharacteristics.I.Health.Value = value;
        }

        private void OnHealthChange(int newValue, int oldValue)
        {
            _health.SetHealth(newValue);
        }

        private void OnMaxHealthChange(int newValue, int oldValue)
        {
            _health.SetMaxHealth(newValue);
        }

        private void OnDestroy()
        {
            var healthValue = PlayerCharacteristics.I.Health;
            var maxValue = PlayerCharacteristics.I.MaxHealth;

            healthValue.OnChanged -= OnHealthChange;
            maxValue.OnChanged -= OnMaxHealthChange;
        }
        private void SpawnPlayer()
        {
            string spawnID = CurrentSessionData.SpawnPosID;

            if (string.IsNullOrEmpty(spawnID))
            {
                return;
            }

            SpawnPointComponent[] spawnPoints = FindObjectsOfType<SpawnPointComponent>();

            foreach (var spawnPoint in spawnPoints)
            {
                if (spawnPoint.ID == spawnID)
                {
                    PlayerCore.Player.transform.position = spawnPoint.SpawnPos;
                    return;
                }
            }

            Debug.LogError($"Spawn point with ID {spawnID} not found!");
        }
    }
}
